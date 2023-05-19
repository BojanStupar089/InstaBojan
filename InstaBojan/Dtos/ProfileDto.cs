
using System.Text.Json.Serialization;

namespace InstaBojan.Dtos
{
    public class ProfileDto
    {
        public string ProfileName { get; set;}
        public string ProfilePicture { get; set;}

       // [JsonIgnore]
        public int UserId { get; set; }
        
        public DateTime BirthDay { get; set;}
        
        public string? Gender {get; set;}

        public int FollowersId {get; set;}

        public int FollowingId {get; set;}


    }
}
