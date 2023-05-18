using AutoMapper;
using InstaBojan.Core.Models;
using InstaBojan.Dtos;

namespace InstaBojan.Mappers.PostMapper
{
    public class PostMapper:IPostMapper
    {

        public Post MapPost(PostDto postDto)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<PostDto, Post>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<PostDto, Post>(postDto);
        }

        public PostDto MapPostDto(Post post)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<Post, PostDto>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<Post, PostDto>(post);
        }

        public List<PostDto> MapListPostsDto(List<Post> posts)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<Core.Models.Post, PostDto>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<List<Post>, List<PostDto>>(posts);

        }

    }
}
