using InstaBojan.Core.Models;
using InstaBojan.Dtos;
using InstaBojan.Dtos.PostsDto;

namespace InstaBojan.Mappers.PostMapper
{
    public interface IPostMapper
    {
        public GetPostsDto MapPostDto(Post post);
        public Post MapPost(AddPostDto postDto);
        public Post MapUpdatePost(UpdatePostDto updatePostDto);
       
    }
}
