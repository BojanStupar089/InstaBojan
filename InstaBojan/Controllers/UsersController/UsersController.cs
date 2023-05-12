using InstaBojan.Core.Models;
using InstaBojan.Infrastructure.Repository.UsersRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaBojan.Controllers.UsersController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersRepository usersRepository;

        public UsersController(UsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        [Authorize(Roles ="USER")]
        [HttpGet]
        public IActionResult GetUsers()
        {

            return Ok(usersRepository.GetUsers());
        }

        [HttpPost]
        public IActionResult Add([FromBody] User user) { 
        
        
            usersRepository.AddUser(user);
            return Ok();
        }

    }
}
