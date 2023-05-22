using System.ComponentModel.DataAnnotations;

namespace InstaBojan.Dtos.UsersDto
{
    public class GetUsersDto
    {

        [Required(ErrorMessage = "FirstName is required.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 20 characters.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Last name must be between 20 characters.")]
        public string LastName { get; set; }

        /*[Required(ErrorMessage = "Email is required.")]
         [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Invalid email address.")]
         [MaxLength(20, ErrorMessage = "Email cannot exceed 20 characters")]*/
        public string Email { get; set; }

        [Required(ErrorMessage = "UserName is required.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "UserName must be between 2 and 20 characters.")]
        public string UserName { get; set; }

        public string Role { get; set; }
    }

}
