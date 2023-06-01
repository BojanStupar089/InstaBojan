
using System.Text.Json.Serialization;

namespace InstaBojan.Dtos.ProfilesDto
{
    public class GetProfilesDto
    {
        public string Name { get; set; }
        public string? ProfilePicture { get; set; }
        public int PostsNumber {get; set;}
        public string Bio { get; set; }
        public int FollowersNumber { get; set; }
        public int FollowingNumber { get; set; }


    }
}
