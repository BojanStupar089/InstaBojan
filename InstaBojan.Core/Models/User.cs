using InstaBojan.Core.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        private string? Username { get; set; }
        private string? Password { get; set; }

        private string? Email { get; set; }

        private Role Role { get; set; }

        private Profile? Profile { get; set; }

    }
}
