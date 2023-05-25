namespace InstaBojan.Dtos.ProfilesDto
{
    public class ProfileDto
    {
        public string ProfileName { get; set; }
        public string ProfilePicture { get; set; }

        public string FirstName {get; set;}

        public string LastName { get; set;}
        
        public DateTime BirthDay { get; set; }

        public string? Gender { get; set; }
    }
}
