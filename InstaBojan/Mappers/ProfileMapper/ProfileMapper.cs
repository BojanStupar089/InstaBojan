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
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<ProfileDto, Core.Models.Profile>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<ProfileDto, Core.Models.Profile>(profileDto);
        }

        public ProfileDto MapProfileDto(Profile profile)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<Core.Models.Profile, ProfileDto>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<Core.Models.Profile, ProfileDto>(profile);
        }
    }
}
