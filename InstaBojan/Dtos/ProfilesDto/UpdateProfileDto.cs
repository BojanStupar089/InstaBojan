﻿namespace InstaBojan.Dtos.ProfilesDto
{
    public class UpdateProfileDto
    {
        public string ProfileName { get; set; }
        public string ProfilePicture { get; set; }

       // public int UserId { get; set; }
        public DateTime BirthDay { get; set; }
        public string? Gender { get; set; }
    }
}
