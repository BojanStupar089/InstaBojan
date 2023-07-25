using InstaBojan.Core.Enums;
using InstaBojan.Core.Models;
using InstaBojan.Core.Security;
using InstaBojan.Infrastructure.Repository.ProfilesRepository;
using InstaBojan.Infrastructure.Repository.UsersRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InstaBojan.Infrastructure.AuthRepository
{
    public class AuthRepository : IAuthRepository
    {

        public IUserRepository _userRepository;
        public IProfilesRepository _profilesRepository;
        public readonly IConfiguration _configuration;

        public AuthRepository(IUserRepository userRepository, IProfilesRepository profilesRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _profilesRepository = profilesRepository;
            _configuration = configuration;
        }

        public string Login(Login login)
        {
            var user = _userRepository.GetUserByUserName(login.UserName);

            if (user == null)
            {

                throw new Exception("wrong Username");
            }

            var token = GenerateToken(user);

            return token;
        }

        public void Register(Register register)
        {

            var profileUser = _profilesRepository.GetProfileByUserName(register.UserName);
            if (profileUser == null)
            {

                var user = new User
                {
                    UserName = register.UserName,
                    Password = BCrypt.Net.BCrypt.HashPassword(register.Password),
                    Email = register.Email,
                    Role = Role.User
                };

                var profile = new Profile
                {
                    ProfileName = register.ProfileName,
                    ProfilePicture = register.ProfilePicture,
                    Birthday = register.BirthDay,
                    Gender = register.Gender,
                    User = user
                };

                _profilesRepository.AddProfile(profile);

            }

            else
            {
                throw new Exception("Username already exists");
            }

        }


        private string GenerateToken(User user)
        {

            var tkn = _configuration.GetSection("JwtToken:SecretKey").Value;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tkn));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: new[] { new Claim(ClaimTypes.Name, user.UserName), new Claim(ClaimTypes.Role, user.Role.ToString()) },

                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            var generatedUser = new JwtSecurityTokenHandler().WriteToken(token);
            return generatedUser;

        }
    }
}
