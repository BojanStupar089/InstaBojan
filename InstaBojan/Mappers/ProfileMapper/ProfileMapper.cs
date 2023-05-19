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

        public Profile MapProfile(AddUpdateProfileDto profileDto)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<AddUpdateProfileDto, Profile>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<Profile>(profileDto);
        }

        public ProfileDto MapProfileDto(Profile profile)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<Profile, ProfileDto>()
            .ForMember(dest => dest.NumberFollowers, opt => opt.MapFrom(src => src.Followers.Count))
            .ForMember(dest=>dest.NumberFollowing,opt=>opt.MapFrom(src=>src.Following.Count)));
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<ProfileDto>(profile);
        }

       
    }
}
