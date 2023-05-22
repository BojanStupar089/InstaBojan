using InstaBojan.Dtos;
using InstaBojan.Dtos.ProfilesDto;
using InstaBojan.Infrastructure.Repository.ProfilesRepository;
using InstaBojan.Mappers.ProfileMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InstaBojan.Controllers.ProfilesController
{
    [Authorize(Roles ="User")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfilesRepository _profilesRepository;
        private readonly IProfileMapper _profileMapper;

        public ProfilesController(IProfilesRepository profilesRepository, IProfileMapper profileMapper)
        {
            _profilesRepository = profilesRepository;
            _profileMapper = profileMapper;
        }

        [HttpGet]
        public IActionResult GetProfiles() {

            var profiles = _profilesRepository.GetProfiles().ToList().Select(p => _profileMapper.MapGetProfilesDto(p));
            if (profiles == null) return NotFound("Profiles is not found");

            return Ok(profiles);
        }

        [HttpGet("{id}")]
        public IActionResult GetProfileById(int id) { 
        
            var profile= _profilesRepository.GetProfileById(id);
            if(profile == null) return NotFound();

            var profileDto=_profileMapper.MapGetProfilesDto(profile);
            return Ok(profileDto);

        }

        [HttpGet("userId")]
        public IActionResult GetProfileByUserId(int userId) { 
        
           var profile=_profilesRepository.GetProfileByUserId(userId);
            if( profile == null) return NotFound();

            var profileDto = _profileMapper.MapGetProfilesDto(profile);
            return Ok(profileDto);
        }

        [HttpGet("username")]
        public IActionResult GetProfileByUserName(string username) { 
        
               var profile=_profilesRepository.GetProfileByUserName(username);
            if(profile == null) return NotFound();

            var profileDto= _profileMapper.MapGetProfilesDto(profile);
            return Ok(profileDto);
        }

        [HttpGet("profilename")]
        public IActionResult GetProfileByProfileName(string profileName) { 
        
            var profile=_profilesRepository.GetProfileByProfileName(profileName);
            if(profile==null) return NotFound();

            var profileDto = _profileMapper.MapGetProfilesDto(profile);
            return Ok(profileDto);

        }

        [HttpPost]
        public IActionResult PostProfiles([FromBody] AddProfileDto profileDto)
        {
            if (!ModelState.IsValid) return BadRequest();
           
           var profile=_profileMapper.MapAddProfile(profileDto);
             _profilesRepository.AddProfile(profile);

           return Created("api/profiles"+"/"+profile.Id, profileDto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProfiles(int id) {

            var username = User.FindFirstValue(ClaimTypes.Name);
            var delProfile=_profilesRepository.GetProfileById(id);
            if(delProfile == null) return NotFound();

            if (delProfile.User.UserName != username) {
                return Forbid();   
            }
            _profilesRepository.DeleteProfile(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProfiles(int id,[FromBody] UpdateProfileDto updateProfileDto) {

            var username = User.FindFirstValue(ClaimTypes.Name); 
            
            
            var profile = _profilesRepository.GetProfileById(id);
            if(profile == null) return NotFound("Profile is not found!");

            if (profile.User.UserName != username) {

                return Forbid();
            }

            var updProfile = _profileMapper.MapUpdateProfile(updateProfileDto);

            _profilesRepository.UpdateProfile(id, updProfile);

            return NoContent();
        }


    }
}
