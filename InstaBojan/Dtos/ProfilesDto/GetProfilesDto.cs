namespace InstaBojan.Dtos.ProfilesDto
{
    public class GetProfilesDto
    {
        public string UserName { get; set; }
        public int PostsNumber { get; set; }
        public int FollowersNumber { get; set; }
        public int FollowingNumber { get; set; }
        public string ProfileName { get; set; }
        public string? ProfilePicture { get; set; }
        public string Bio { get; set; }
    }
}
