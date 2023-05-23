using InstaBojan.Core.Models;
using InstaBojan.Dtos;
using InstaBojan.Dtos.PostsDto;

namespace InstaBojan.Mappers.PostMapper
{
    public interface IPostMapper
    {
        public GetPostsDto MapGetPostsDto(Post post);
        public Post MapAddPost(AddPostDto postDto);
        public Post MapUpdatePost(UpdatePostDto updatePostDto);
       
    }
}
