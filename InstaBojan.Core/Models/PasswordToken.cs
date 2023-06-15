using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Core.Models
{
    public class PasswordToken
    {

        public string Token { get; set;}

        public string Email { get; set;}

        public DateTime CreatedAt {get; set;}
    }
}
