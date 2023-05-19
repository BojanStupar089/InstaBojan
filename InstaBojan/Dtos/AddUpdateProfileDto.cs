namespace InstaBojan.Dtos
{
    public class AddUpdateProfileDto
    {
        public string ProfileName { get; set; }
        public string ProfilePicture { get; set; }

        // [JsonIgnore]
        public int UserId { get; set; }

        public DateTime BirthDay { get; set; }

        public string? Gender { get; set; }
    }
}
