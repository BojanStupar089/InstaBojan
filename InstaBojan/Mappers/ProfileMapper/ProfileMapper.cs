using AutoMapper;
using InstaBojan.Core.Models;
using InstaBojan.Dtos;
using Profile = InstaBojan.Core.Models.Profile;

namespace InstaBojan.Mappers.ProfileMapper
{
    public class ProfileMapper : IProfileMapper
    {
        public List<ProfileDto> MapListProfilesDto(List<Profile> profiles)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<Profile, ProfileDto>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<List<Profile>, List<ProfileDto>>(profiles);
        }

        public Profile MapProfile(ProfileDto profileDto)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<ProfileDto, Profile>()
            .ForMember(source=>source.Followers,opt=>opt.MapFrom(dest=>dest.FollowersId))
            .ForMember(source=>source.Following,opt=>opt.MapFrom(dest=>dest.FollowingId)));
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<ProfileDto,Profile>(profileDto);
        }

        public ProfileDto MapProfileDto(Profile profile)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<Profile, ProfileDto>()
            .ForMember(dest => dest.FollowersId, opt => opt.MapFrom(src => src.Followers.Count))
            .ForMember(dest=>dest.FollowingId,opt=>opt.MapFrom(src=>src.Following.Count)));
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<Profile, ProfileDto>(profile);
        }
    }
}
