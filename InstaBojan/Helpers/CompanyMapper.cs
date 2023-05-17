using AutoMapper;
using InstaBojan.Core.Models;
using InstaBojan.Core.Security;
using InstaBojan.Dtos;

namespace InstaBojan.Helpers
{
    public class CompanyMapper:ICompanyMapper
    {

        public CompanyMapper() { 
        
           
        }

        public Post MapPost(PostDto postDto)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<PostDto, Post>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<PostDto, Post>(postDto);
        }

        public PostDto MapPostDto(Post post)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<Post,PostDto>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<Post,PostDto>(post);
        }

        public User MapUser(UserDto userDto)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<UserDto, User>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<UserDto, User>(userDto);
        }

        public UserDto MapUserDto(User user)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDto>());
            Mapper mapper=new Mapper(configuration);

            return mapper.Map<User, UserDto>(user);
        }

        public Core.Models.Profile MapProfile(ProfileDto profileDto)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<ProfileDto, Core.Models.Profile>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<ProfileDto, Core.Models.Profile>(profileDto);
        }

        public ProfileDto MapProfileDto(Core.Models.Profile profile)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<Core.Models.Profile,ProfileDto>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<Core.Models.Profile, ProfileDto>(profile);
        }
    }
}
