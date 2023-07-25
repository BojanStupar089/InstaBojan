namespace InstaBojan.Dtos.PostsDto
{
    public class PostDto
    {

        public int Id { get; set; }  // from PostContainer postcontext.provider
        public string UserName { get; set; } //from PostContainer PostHeader

        public string UserProfilePicture { get; set; }//from PostContainer PostHeader userProfilePicture

        public string Text { get; set; } //text.
        public string Picture { get; set; } //post picture from PostContainer img 

        // public DateTime DateTime { get; set; }//from PostContainer TagRibbon

        //public int NumOfReactions { get; set; }//from PostContainer reactionsBar
        //public string? Location {get; set;}
        //public List<string>? Categories {get; set;}

        // public DateTime? Time {get; set;}
        //public int NumOfShares {get; set;}
        // public bool Viral {get; set;}
    }
}
