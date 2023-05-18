using BCrypt.Net;
using InstaBojan.Core.Models;
using InstaBojan.Dtos;
using InstaBojan.Infrastructure.Repository.UsersRepository;
using InstaBojan.Mappers.UserMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaBojan.Controllers.UsersController
{
   // [Authorize(Roles ="User")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly IUserRepository usersRepository;
        public IUserMapper _mapper;

        public UsersController(IUserRepository usersRepository, IUserMapper mapper)
        {
            this.usersRepository = usersRepository;
            this._mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = usersRepository.GetUsers();
            if (users == null) 
                return BadRequest("Users are null");
           
            return Ok(usersRepository.GetUsers());
        }


        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {

            var user = usersRepository.GetUserById(id);
            if (user == null)
            {

                return NotFound("User doesn't exist");
            }

            var userDto = _mapper.MapUserDto(user);
            return Ok(userDto);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {

            var userDel = usersRepository.GetUserById(id);
            if (userDel == null)
            {
                return NotFound("User doesn't exist");
            }

            usersRepository.DeleteUser(id);
            return NoContent();
        }

        [HttpGet("username")]
        public IActionResult GetUsername(string username)
        {

            var user = usersRepository.GetUserByUserName(username);

            if (user == null)
            {
                return NotFound("User doesn't exist");
            }

            usersRepository.GetUserByUserName(user.UserName);
            return Ok(user);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDto userDto)
        {

           var user = usersRepository.GetUserById(id);

            if (user == null)
            {

                return NotFound("User doesn't exist");
            }

             var updUser = _mapper.MapUser(userDto);
            // usersRepository.UpdateUser(updUser);

           usersRepository.UpdateUser(id, updUser);

            return NoContent();
        }

    }
}
