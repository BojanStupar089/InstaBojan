using AutoMapper;
using InstaBojan.Core.Models;
using InstaBojan.Dtos;
using InstaBojan.Dtos.ProfilesDto;
using Profile = InstaBojan.Core.Models.Profile;

namespace InstaBojan.Mappers.ProfileMapper
{
    public class ProfileMapper : IProfileMapper
    {
     

        public Profile MapAddProfile(AddProfileDto profileDto)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<AddProfileDto, Profile>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<Profile>(profileDto);
        }

      

        public GetProfilesDto MapGetProfilesDto(Profile profile)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<Profile, GetProfilesDto>()
            .ForMember(dest => dest.NumberFollowers, opt => opt.MapFrom(src => src.Followers.Count))
            .ForMember(dest=>dest.NumberFollowing,opt=>opt.MapFrom(src=>src.Following.Count)));
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<GetProfilesDto>(profile);
        }

       

        public Profile MapUpdateProfile(UpdateProfileDto updateProfileDto)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<UpdateProfileDto, Profile>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<Profile>(updateProfileDto);
        }
    }
}
