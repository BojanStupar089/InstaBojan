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

        private IConfiguration _configuration;
        public AuthController(IUserRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }


        [HttpPost("login")]
        public IActionResult Login(LoginModel loginModel)
        {

            var user = _repository.GetUserByUserName(loginModel.UserName);
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginModel.Password, user.Password);

            if (user == null || !isValidPassword)
            {

                return BadRequest("Wrong credentials");
            }

            //Authenticate user and generate JWT token
            var token = GenerateToken(user);

            //return JWT token

            return Ok(new { token });
        }

        [HttpPost("register")]
        public IActionResult Register(string firstName,string lastName,string email,string userName,string password)
        {

            var user = _repository.GetUserByUserName(userName);

            if (user != null)
            {

                return BadRequest("User already exists");
            }

            user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email =email,
                UserName = userName,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                Role = Role.User,
            };

            _repository.AddUser(user);

            // var token = GenerateToken(user);

             return Ok();

           

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

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
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
