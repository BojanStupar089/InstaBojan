using AutoMapper;
using InstaBojan.Core.Models;
using InstaBojan.Dtos;
using InstaBojan.Dtos.ProfilesDto;
using Profile = InstaBojan.Core.Models.Profile;

namespace InstaBojan.Mappers.ProfileMapper
{
    public class ProfileMapper : IProfileMapper
    {


        public Profile MapProfile(ProfileDto profileDto)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<ProfileDto, Profile>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<Profile>(profileDto);
        }



        public GetProfilesDto MapGetProfilesDto(Profile profile)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<Profile, GetProfilesDto>()
            .ForMember(dest => dest.FollowersNumber, opt => opt.MapFrom(src => src.Followers.Count))
            .ForMember(dest => dest.PostsNumber, opt => opt.MapFrom(src => src.Posts.Count))
            .ForMember(dest => dest.FollowingNumber, opt => opt.MapFrom(src => src.Following.Count))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProfileName))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName)));
            

            Mapper mapper = new Mapper(configuration);

            return mapper.Map<GetProfilesDto>(profile);
        }


    }
}
