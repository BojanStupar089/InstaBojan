using System.Text.Json.Serialization;

namespace InstaBojan.Dtos
{
    public class PostDto
    {
        public string Picture { get; set;}
        [JsonIgnore]
        public int ProfileId { get; set;}
        public string Text { get; set;}
    }
}
