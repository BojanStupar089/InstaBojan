using NHibernate.Type;
using System.Runtime.InteropServices;
using System;

namespace InstaBojan.Dtos.PostsDto
{
    public class PostDto
    {

        public int Id {get; set;}  // from PostContainer postcontext.provider
        public String UserName {get; set;} //from PostContainer PostHeader
       
        public String UserProfilePicture { get; set; }//from PostContainer PostHeader userProfilePicture
       
        public String Text { get; set; } //text.
        public String Picture { get; set; } //post picture from PostContainer img 
        public DateTime DateTime { get; set; }//from PostContainer TagRibbon
       
        public int NumOfReactions { get; set; }//from PostContainer reactionsBar
        private String Location {get; set;}
        private List<String> Categories {get; set;}
        
        private DateTime Time {get; set;}
        private int NumOfShares {get; set;}
        private bool Viral {get; set;}
    }
}
