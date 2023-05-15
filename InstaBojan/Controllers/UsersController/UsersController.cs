using BCrypt.Net;
using InstaBojan.Core.Models;
using InstaBojan.Dtos;
using InstaBojan.Helpers;
using InstaBojan.Infrastructure.Repository.UsersRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaBojan.Controllers.UsersController
{
    //[Authorize(Roles ="User")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly IUserRepository usersRepository;
        public ICompanyMapper _mapper;

        public UsersController(IUserRepository usersRepository, ICompanyMapper mapper)
        {
            this.usersRepository = usersRepository;
            this._mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetUsers()
        {

            return Ok(usersRepository.GetUsers());
        }


        [HttpGet("id")]
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


        [HttpDelete("id")]
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

        [HttpPut("id")]
        public IActionResult UpdateUser([FromQuery]int id, [FromBody] UserDto userDto)
        {


            var user = usersRepository.GetUserById(id);

            if (user == null)
            {

                return NotFound("User doesn't exist");
            }

             var updUser = _mapper.MapUser(userDto);
             usersRepository.UpdateUser(updUser);
            

           /* user.Id = userr.Id;
            user.FirstName=userr.FirstName;
            user.LastName=userr.LastName;
            user.Email=userr.Email;
            user.UserName = userr.UserName;
            user.Password=BCrypt.Net.BCrypt.HashPassword(userr.Password);
           
            usersRepository.UpdateUser(user);
           */


          return NoContent();
        }

    }
}
