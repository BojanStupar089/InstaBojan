

namespace InstaBojan.Dtos
{
    public class RegistrationDto
    {

        public string UserName { get; set; }
        public string Password { get; set; }
       
        public DateTime BirthDay { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Name {get; set;}
       
       
        public  string? ProfilePicture { get; set; }

    }
}
