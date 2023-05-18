using InstaBojan.Core.Models;
using InstaBojan.Dtos;

namespace InstaBojan.Mappers.PostMapper
{
    public interface IPostMapper
    {
        public PostDto MapPostDto(Post post);
        public Post MapPost(PostDto postDto);
        List<PostDto> MapListPostsDto(List<Post> Posts);
    }
}
