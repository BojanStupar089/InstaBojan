using AutoMapper;
using InstaBojan.Core.Models;
using InstaBojan.Dtos;
using InstaBojan.Dtos.PostsDto;

namespace InstaBojan.Mappers.PostMapper
{
    public class PostMapper:IPostMapper
    {

        public Post MapAddPost(AddPostDto postDto)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<AddPostDto, Post>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<AddPostDto, Post>(postDto);
        }

        public GetPostsDto MapGetPostsDto(Post post)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<Post, GetPostsDto>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<GetPostsDto>(post);
        }

        public Post MapUpdatePost(UpdatePostDto updatePostDto)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<UpdatePostDto, Post>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<Post>(updatePostDto);
        }
    }
}
