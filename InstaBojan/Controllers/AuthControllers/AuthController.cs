using Azure;
using InstaBojan.Core.Enums;
using InstaBojan.Core.Security;
using InstaBojan.Dtos.RegisterDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Response = InstaBojan.Core.Security.Response;

namespace InstaBojan.Controllers.AuthControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;


        public AuthController(UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole)); 
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var userExists = await _userManager.FindByNameAsync(registerDto.UserName);
            if (userExists != null)
                /* return StatusCode(StatusCodes.Status500InternalServerError, new Core.Security.Response { Status = "Error", Message = "User already exists!" });*/

            return BadRequest("User already exists");
            IdentityUser user = new()
            {
                Email = registerDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName =registerDto.UserName,
              

                
            };

            var passwordHash=BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            var result = await _userManager.CreateAsync(user,passwordHash);

            if (!result.Succeeded)
                /*return StatusCode(StatusCodes.Status500InternalServerError, new Core.Security.Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });*/
                return BadRequest("User creation failed! Please check user details and try again.");
            
            return Ok(new Core.Security.Response { Status="Success", Message="User created successfully!"});
        }


        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto registerDto)
        {
            var userExists = await _userManager.FindByNameAsync(registerDto.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Core.Security.Response { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                Email = registerDto.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerDto.UserName
            };
            var result = await _userManager.CreateAsync(user, registerDto.UserName);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            var adminRole = Role.Admin;
            var userRole = Role.User;
           
            if (!await _roleManager.RoleExistsAsync(adminRole.ToString()))
                await _roleManager.CreateAsync(new IdentityRole(adminRole.ToString()));
            
            if (!await _roleManager.RoleExistsAsync(userRole.ToString()))
                await _roleManager.CreateAsync(new IdentityRole(userRole.ToString()));

            if (await _roleManager.RoleExistsAsync(adminRole.ToString()))
            {
                await _userManager.AddToRoleAsync(user, adminRole.ToString());
            }
           
            if (await _roleManager.RoleExistsAsync(adminRole.ToString()))
            {
                await _userManager.AddToRoleAsync(user,userRole.ToString());
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }



        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }


    }
}
