using InstaBojan.Dtos.UsersDto;
using InstaBojan.Infrastructure.Repository.UsersRepository;
using InstaBojan.Mappers.UserMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InstaBojan.Controllers.UsersController
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly IUserRepository _userRepository;
        public IUserMapper _mapper;

        public UsersController(IUserRepository usersRepository, IUserMapper mapper)
        {
            this._userRepository = usersRepository;
            this._mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        // [Authorize(Roles ="User")]
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers().Select(u => _mapper.MapUserDto(u));
            if (users == null) return NotFound();

            return Ok(users);
        }

        [Authorize(Roles = "Admin")]

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {

            var user = _userRepository.GetUserById(id);
            if (user == null)
            {

                return NotFound("User doesn't exist");
            }

            var userDto = _mapper.MapUserDto(user);
            return Ok(userDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("username")]
        public IActionResult GetUsername(string username)
        {

            var user = _userRepository.GetUserByUserName(username);

            if (user == null)
            {
                return NotFound("User doesn't exist");
            }


            return Ok(user);

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("email")]
        public IActionResult GetEmail(string email)
        {

            var user = _userRepository.GetUserByEmail(email);
            if (user == null) return NotFound("User doesn't exist");

            var userDto = _mapper.MapUserDto(user);
            return Ok(userDto);
        }

        /*
        [Authorize(Roles = "User,Admin")]
        [HttpPost("reset-password")]
        public IActionResult SendEmailForResetPasswordToken([FromQuery]string email)
        {
            _userRepository.SendMailForResetPassword(email);
            return Ok("Password reset email sent successfully");
        }

        */

        [Authorize(Roles = "User,Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {

            var userDel = _userRepository.GetUserById(id);

            if (userDel == null)
            {
                return NotFound("User doesn't exist");
            }
            var username = User.FindFirstValue(ClaimTypes.Name);

            if (username != userDel.UserName && !User.IsInRole("Admin"))
            {

                return Forbid();
            }

            _userRepository.DeleteUser(id);

            return NoContent();
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDto userDto)
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            if (username == null) return NotFound();

            var user = _userRepository.GetUserById(id);



            if (user == null)
            {

                return NotFound("User doesn't exist");
            }

            if (username != user.UserName && !User.IsInRole("Admin"))
            {

                return Forbid();
            }

            var userNameExist = _userRepository.GetUserByUserName(userDto.UserName);

            if (userNameExist != null && userNameExist.Id != user.Id)
            {
                return BadRequest("Username already exists");
            }


            var updUser = _mapper.MapUser(userDto);
            _userRepository.UpdateUser(id, updUser);

            return NoContent();
        }

    }
}
