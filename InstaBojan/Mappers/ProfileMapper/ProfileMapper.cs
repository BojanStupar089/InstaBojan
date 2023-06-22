using AutoMapper;
using InstaBojan.Core.Models;
using InstaBojan.Dtos;
using InstaBojan.Dtos.PostsDto;
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

        public List<UserSearchResultDto> MapUserSearchResultDto(List<Profile> profiles)
        {

            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<Profile, UserSearchResultDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProfileName)));
            Mapper mapper = new Mapper(configuration);

            List<UserSearchResultDto> userSearchResultDtos = mapper.Map<List<UserSearchResultDto>>(profiles);
            return userSearchResultDtos;
        }

        public Profile MapUpdateProfile(UpdateProfileDto updateProfileDto)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<UpdateProfileDto, Profile>()
            .ForPath(dest=>dest.User.UserName,opt=>opt.MapFrom(src=>src.UserName))
            .ForMember(dest=>dest.ProfileName,opt=>opt.MapFrom(src=>src.Name)));
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<Profile>(updateProfileDto);
        }
    }
}
