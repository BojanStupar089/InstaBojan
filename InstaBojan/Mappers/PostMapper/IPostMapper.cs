using InstaBojan.Core.Models;
using InstaBojan.Dtos.PostsDto;

namespace InstaBojan.Mappers.PostMapper
{
    public interface IPostMapper
    {
        public PostDto MapGetPostDto(Post post);
        public Post MapPost(PostDto postDto);

        public Post MapNewPost(NewPostDto postDto);
        public List<PostDto> MapListPostDto(List<Post> posts);

        public Post MapUpdatePost(UpdatePostDto postDto);

        public IEnumerable<PostDto> MapEnumPostDto(IEnumerable<Post> posts);








    }
}
