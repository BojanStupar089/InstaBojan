using System.ComponentModel.DataAnnotations;

namespace InstaBojan.Dtos.UsersDto
{
    public class GetUsersDto
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }

}
