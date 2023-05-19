using InstaBojan.Dtos;
using InstaBojan.Infrastructure.Repository.ProfilesRepository;
using InstaBojan.Mappers.ProfileMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaBojan.Controllers.ProfilesController
{
   // [Authorize(Roles ="User")]
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
        
           var profiles=_profilesRepository.GetProfiles();
            if (profiles == null) return NotFound();

            var listProfilesDto = _profileMapper.MapListProfilesDto(profiles);
            return Ok(listProfilesDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetProfileById(int id) { 
        
            var profile= _profilesRepository.GetProfileById(id);
            if(profile == null) return NotFound();

            var profileDto=_profileMapper.MapProfileDto(profile);
            return Ok(profileDto);

        }

        [HttpGet("userId")]
        public IActionResult GetProfileByUserId(int userId) { 
        
           var profile=_profilesRepository.GetProfileByUserId(userId);
            if( profile == null) return NotFound();

            var profileDto = _profileMapper.MapProfileDto(profile);
            return Ok(profileDto);
        }

        [HttpGet("username")]
        public IActionResult GetProfileByUserName(string username) { 
        
               var profile=_profilesRepository.GetProfileByUserName(username);
            if(profile == null) return NotFound();

            var profileDto= _profileMapper.MapProfileDto(profile);
            return Ok(profileDto);
        }

        [HttpGet("profilename")]
        public IActionResult GetProfileByProfileName(string profileName) { 
        
            var profile=_profilesRepository.GetProfileByProfileName(profileName);
            if(profile==null) return NotFound();

            var profileDto = _profileMapper.MapProfileDto(profile);
            return Ok(profileDto);

        }

        [HttpPost]
        public IActionResult PostProfiles([FromBody] ProfileDto profileDto)
        {
            if (!ModelState.IsValid) return BadRequest();
           
           var profile=_profileMapper.MapProfile(profileDto);
             _profilesRepository.AddProfile(profile);

           return Created("api/profiles"+"/"+profile.Id, profileDto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProfiles(int id) { 
        
          var delProfile=_profilesRepository.GetProfileById(id);
            if(delProfile == null) return NotFound();

            _profilesRepository.DeleteProfile(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProfiles(int id, [FromBody] ProfileDto profileDto) { 
        
              var updProfile= _profilesRepository.GetProfileById(id);
            if(updProfile == null) return NotFound();

            var updateProfil = _profileMapper.MapProfile(profileDto);
            _profilesRepository.UpdateProfile(id, updateProfil);

            return NoContent();
        }


    }
}
