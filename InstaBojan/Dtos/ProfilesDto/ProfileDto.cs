using InstaBojan.Dtos.UsersDto;

namespace InstaBojan.Dtos.ProfilesDto
{
    public class ProfileDto
    {
        public string? ProfileName { get; set; }
        public string? ProfilePicture { get; set; }
        public string Name {get; set;}
      

        public UserDto UserDto { get; set; }
        
        public DateTime BirthDay { get; set; }

        public string? Gender { get; set; }
    }
}
