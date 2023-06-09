﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Core.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string ProfileName { get; set; }
        public string? ProfilePicture { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime Birthday { get; set; }

        [JsonIgnore]
        public List<Post>? Posts { get; set;}
        public string? Gender { get; set; }
        [JsonIgnore]
        public List<Profile>? Followers { get; set; }
        [JsonIgnore]
        public List<Profile>? Following { get; set; }
       
    }
}
