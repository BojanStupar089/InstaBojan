using InstaBojan.Core.Models;
using InstaBojan.Dtos;
using InstaBojan.Dtos.PostsDto;

namespace InstaBojan.Mappers.PostMapper
{
    public interface IPostMapper
    {
        public PostDto MapGetPostDto(Post post);
        public Post MapPost(PostDto postDto);
      
       
    }
}
