using InstaBojan.Core.Models;
using InstaBojan.Dtos;

namespace InstaBojan.Mappers.ProfileMapper
{
    public interface IProfileMapper
    {
        public ProfileDto MapProfileDto(Profile profile);
        public Profile MapProfile(AddUpdateProfileDto profileDto);
        List<ProfileDto> MapListProfilesDto(List<Profile> profiles);
       
    }
}
