using InstaBojan.Core.Models;
using InstaBojan.Dtos.ProfilesDto;

namespace InstaBojan.Mappers.ProfileMapper
{
    public interface IProfileMapper
    {

        public GetProfilesDto MapGetProfilesDto(Profile profile);
        public List<UserSearchResultDto> MapUserSearchResultDto(List<Profile> profiles);
        public Profile MapUpdateProfile(UpdateProfileDto updateProfileDto);



    }
}
