namespace InstaBojan.Dtos
{
    public class ProfileDto
    {
        public string ProfileName { get; set;}
        public string ProfilePicture { get; set;}
        public int UserId { get; set; }
        public string BirthDay { get; set;}
        public List<PostDto> Posts {get; set;}
        public string Gender {get; set;}

        public List<ProfileDto> Followers {get; set;}
        public List<ProfileDto> Following {get; set;}


    }
}
