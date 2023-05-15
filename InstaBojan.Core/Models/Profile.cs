using InstaBojan.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Core.Models
{
    public class Profile
    {
        private int Id { get; set; }
        private string? Name { get; set; }
        private string? ProfilePicture { get; set; }
        private User? UserId { get; set; }
        private User? User { get; set; }
        private DateTime Birthday { get; set; }
        private List<Post>? Posts { get; set; }

        private string? Gender { get; set; }
        private List<Profile>? Followers { get; set; }
        private List<Profile>? Following { get; set; }


    }
}
