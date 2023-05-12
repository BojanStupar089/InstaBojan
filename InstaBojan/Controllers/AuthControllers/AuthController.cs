using InstaBojan.Core.Models;
using InstaBojan.Core.Security;
using InstaBojan.Infrastructure.Repository.UsersRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        private readonly IUserRepository _repository;
        public AuthController(IUserRepository repository)
        {
            _repository = repository;
        }


        [HttpPost("login")]
        public IActionResult Login(LoginModel loginModel)
        {

            User user = _repository.GetUserByUserName(loginModel.UserName);

            if (user == null || !user.Password.Equals(loginModel.Password))
            {

                return BadRequest("Wrong credentials");
            }

            //Authenticate user and generate JWT token
            var token = GenerateToken(user);

            //return JWT token

            return Ok(new { token });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User regUser)
        {

            var user = _repository.GetUserByUserName(regUser.UserName);

            if (user != null)
            {

                return BadRequest("User already exists");
            }

            user = new User
            {
                FirstName = regUser.FirstName,
                LastName = regUser.LastName,
                Email = regUser.Email,
                Role = Role.User,
                UserName = regUser.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(regUser.Password)
            };

            _repository.AddUser(user);

            // var token = GenerateToken(user);

            // return Ok(new { token });

            return Ok();

        }

        // da proveris imas 2 metode 



       [Authorize(Roles = "ADMIN")]
        [HttpGet("test-admin-authorization")]
        public async Task<IActionResult> TestAuth()
        {
            return Ok("Success");
        }

        [Authorize(Roles = "USER")]
        [HttpGet("test-regular-authorization")]
        public async Task<IActionResult> TestRegAuth()
        {
            return Ok("Success");
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

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
