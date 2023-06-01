using InstaBojan.Core.Models;
using InstaBojan.Dtos;
using InstaBojan.Dtos.ProfilesDto;

namespace InstaBojan.Mappers.ProfileMapper
{
    public interface IProfileMapper
    {
        public Profile MapProfile(ProfileDto profileDto);
        public GetProfilesDto MapGetProfilesDto(Profile profile);

        
       
      
       
    }
}
