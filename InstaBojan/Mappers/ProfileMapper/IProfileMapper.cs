using InstaBojan.Core.Models;
using InstaBojan.Dtos;
using InstaBojan.Dtos.ProfilesDto;

namespace InstaBojan.Mappers.ProfileMapper
{
    public interface IProfileMapper
    {
        public Profile MapUpdateProfile(UpdateProfileDto updateProfileDto);
        public Profile MapAddProfile(AddProfileDto profileDto);

        public GetProfilesDto MapGetProfilesDto(Profile profile);
      
       
    }
}
