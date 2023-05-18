using InstaBojan.Core.Models;
using InstaBojan.Dtos;

namespace InstaBojan.Mappers.ProfileMapper
{
    public interface IProfileMapper
    {
        public ProfileDto MapProfileDto(Profile profile);
        public Profile MapProfile(ProfileDto profileDto);
        List<ProfileDto> MapListProfilesDto(List<Profile> profiles);
       
    }
}
