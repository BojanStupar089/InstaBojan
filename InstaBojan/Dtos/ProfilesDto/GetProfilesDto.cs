
using System.Text.Json.Serialization;

namespace InstaBojan.Dtos.ProfilesDto
{
    public class GetProfilesDto
    {
        public string ProfileName { get; set; }
        public string ProfilePicture { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string? Gender { get; set; }
        public int NumberFollowers { get; set; }
        public int NumberFollowing { get; set; }


    }
}
