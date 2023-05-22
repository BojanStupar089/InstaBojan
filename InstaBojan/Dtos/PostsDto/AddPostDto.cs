using System.Text.Json.Serialization;

namespace InstaBojan.Dtos.PostsDto
{
    public class AddPostDto
    {
        public string Picture { get; set; }
        public int ProfileId { get; set; }
        public string Text { get; set; }
    }
}
