using InstaBojan.Core.Security;
using InstaBojan.Infrastructure.AuthRepository;
using InstaBojan.Infrastructure.Repository.ProfilesRepository;
using InstaBojan.Infrastructure.Repository.UsersRepository;
using Microsoft.AspNetCore.Mvc;

namespace InstaBojan.Controllers.AuthControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IProfilesRepository _profilesRepository;
        private readonly IUserRepository _userRepository;
        private IAuthRepository _authRepository;


        public AuthController(IProfilesRepository repository, IUserRepository userRepository, IAuthRepository authRepository)
        {
            _profilesRepository = repository;
            _userRepository = userRepository;
            _authRepository = authRepository;

        }


        [HttpPost("login")]
        public IActionResult Login(Login login)
        {

            var user = _userRepository.GetUserByUserName(login.UserName);

            if (user == null)
            {
                return BadRequest("Invalid username");
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(login.Password, user.Password);

            if (!isValidPassword)
            {

                return BadRequest("Wrong password");
            }

            var token = _authRepository.Login(login);

            return Ok(new { token });
        }

        [HttpPost("registration")]
        public IActionResult Register([FromBody] Register register)
        {

            var profile = _profilesRepository.GetProfileByUserName(register.UserName);

            if (profile != null)
            {
                return BadRequest("Username already exists");
            }

            _authRepository.Register(register);

            return Ok("Your profile has been successfully created");



        }





    }
}
