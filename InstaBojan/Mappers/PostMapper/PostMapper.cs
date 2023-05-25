using AutoMapper;
using InstaBojan.Core.Models;
using InstaBojan.Dtos;
using InstaBojan.Dtos.PostsDto;
using Microsoft.Extensions.Hosting;

namespace InstaBojan.Mappers.PostMapper
{
    public class PostMapper:IPostMapper
    {

         public PostDto MapGetPostDto(Post post)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<Post, PostDto>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<PostDto>(post);
        }

       

        public Post MapPost(PostDto postDto)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<PostDto, Post>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<Post>(postDto);
        }
    }
}
