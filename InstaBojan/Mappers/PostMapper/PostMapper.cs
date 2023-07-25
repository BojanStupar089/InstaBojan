using AutoMapper;
using InstaBojan.Core.Models;
using InstaBojan.Dtos.PostsDto;

namespace InstaBojan.Mappers.PostMapper
{
    public class PostMapper : IPostMapper
    {

        public PostDto MapGetPostDto(Post post)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<Post, PostDto>()
             .ForMember(dest => dest.UserProfilePicture, opt => opt.MapFrom(src => src.Publisher.ProfilePicture))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Publisher.User.UserName)));
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<PostDto>(post);
        }

        public Post MapNewPost(NewPostDto postDto)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<NewPostDto, Post>());

            Mapper mapper = new Mapper(configuration);

            return mapper.Map<Post>(postDto);
        }

        public Post MapPost(PostDto postDto)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<PostDto, Post>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<Post>(postDto);
        }

        public List<PostDto> MapListPostDto(List<Post> posts)
        {

            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<Post, PostDto>()
            .ForMember(dest => dest.UserProfilePicture, opt => opt.MapFrom(src => src.Publisher.ProfilePicture))
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Publisher.User.UserName)));
            Mapper mapper = new Mapper(configuration);

            List<PostDto> postDtos = mapper.Map<List<PostDto>>(posts);
            return postDtos;

        }

        public IEnumerable<PostDto> MapEnumPostDto(IEnumerable<Post> posts)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Post, PostDto>()
                .ForMember(dest => dest.UserProfilePicture, opt => opt.MapFrom(src => src.Publisher.ProfilePicture))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Publisher.User.UserName));


            });

            Mapper mapper = new Mapper(configuration);

            IEnumerable<PostDto> postDtos = mapper.Map<IEnumerable<PostDto>>(posts);
            return postDtos;
        }

        public Post MapUpdatePost(UpdatePostDto postDto)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<UpdatePostDto, Post>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<Post>(postDto);
        }



    }
}
