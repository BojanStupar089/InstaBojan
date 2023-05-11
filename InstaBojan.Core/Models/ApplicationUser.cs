using InstaBojan.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Core.Models
{
    public class ApplicationUser:IdentityUser
    {

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        private string UserName { get; set; }
        private string Password { get; set; }

        private string Email { get; set; }

        private Role Role { get; set; }

        private Profile Profile { get; set; }
     

      
    }
}
