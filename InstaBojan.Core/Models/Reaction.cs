
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Core.Models
{
    public class Reaction
    {
        public int Id { get; set; }
        public string ReactionName {get; set;}
        //public int PostFk {get; set;}
        public Post Post {get; set;}
       // public int ProfileFk {get; set;}
        public Profile Profile {get; set;}


    }
}
