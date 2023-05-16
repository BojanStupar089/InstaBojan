using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Core.Models
{
    public class Post
    {
       public int Id { get; set; }
       public string Picture { get; set; }
       //public int ProfileFk { get; set; }
       public Profile Publisher { get; set; }
       public string Text { get; set; }
       public List<Reaction> Reactions {get; set;}

      
    }
}
