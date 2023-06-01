using InstaBojan.Core.Models;
using InstaBojan.Core.Security;
using InstaBojan.Dtos;
using InstaBojan.Dtos.ProfilesDto;
using InstaBojan.Dtos.UsersDto;
using InstaBojan.Infrastructure.Repository.ProfilesRepository;
using InstaBojan.Infrastructure.Repository.UsersRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InstaBojan.Controllers.AuthControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IProfilesRepository _profilesRepository;
        private readonly IUserRepository _userRepository;


        public AuthController(IProfilesRepository repository,IUserRepository userRepository)
        {
            _profilesRepository = repository;
            _userRepository = userRepository;

        }


        [HttpPost("login")]
        public IActionResult Login(LoginModel loginModel)
        {

            var user = _userRepository.GetUserByUserName(loginModel.UserName);

            if (user == null)
            {
                return BadRequest("Invalid username");
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginModel.Password, user.Password);

            if (!isValidPassword)
            {

                return BadRequest("Wrong password");
            }

            //Authenticate user and generate JWT token
            var token = GenerateToken(user);

            //return JWT token

            return Ok(new { token });
        }

        [HttpPost("registration")]
        public IActionResult Register([FromBody] RegistrationDto registrationDto)
        {

           var profile = _profilesRepository.GetProfileByUserName(registrationDto.UserName);

            var user = new User
            {
                UserName = registrationDto.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(registrationDto.Password),
                Email = registrationDto.Email,
                Role =Role.User

            };
            
            if (profile == null)
            {
                profile = new Profile
                {
                    ProfileName =registrationDto.Name,
                    ProfilePicture=registrationDto.ProfilePicture,
                    Birthday = registrationDto.BirthDay,
                    Gender = registrationDto.Gender,
                    User=user
                    
               };
                 
                 _profilesRepository.AddProfile(profile);
                 

                return Ok("Profile  created successfully");
            }
            else
                return BadRequest("Already exists");


        }

        // da proveris imas 2 metode 



        [Authorize(Roles = "Admin")]
        [HttpGet("test-admin-authorization")]
        public async Task<IActionResult> TestAuth()
        {
            return Ok("Success Admin");
        }

        [Authorize(Roles = "User")]
        [HttpGet("test-regular-authorization")]
        public async Task<IActionResult> TestRegAuth()
        {
            return Ok("Success User");
        }








        private string GenerateToken(User user)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-long-secret-key"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: new[] { new Claim(ClaimTypes.Name, user.UserName), new Claim(ClaimTypes.Role, user.Role.ToString()) },

                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            var generatedUser = new JwtSecurityTokenHandler().WriteToken(token);

            if (TokenBlackList.IsTokenBlackListed(generatedUser))
            {
                return ("Token is blacklisted");
            }

            return generatedUser;

        }
    }
}
