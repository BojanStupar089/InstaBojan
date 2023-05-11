using InstaBojan.Core.Enums;
using InstaBojan.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Core.Security
{
    public class RegisterModel
    {

        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }


        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


        private Role Role { get; set; }

        private Profile Profile { get; set; }
    }
}
