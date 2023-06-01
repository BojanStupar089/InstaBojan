using InstaBojan.Dtos.CatDto;
using System.Text.Json.Serialization;

namespace InstaBojan.Dtos.PostsDto
{
    public class NewPostDto
    {
        public string? Picture { get; set; }
        public string? Text { get; set; }
        public LocationDto? Location { get; set; }
        public List<string>? Category { get; set; }
    }
}
