using InstaBojan.Dtos;
using InstaBojan.Helpers;
using InstaBojan.Infrastructure.Repository.ProfilesRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaBojan.Controllers.ProfilesController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfilesRepository _profilesRepository;
        private readonly ICompanyMapper _companyMapper;

        public ProfilesController(IProfilesRepository profilesRepository, ICompanyMapper companyMapper)
        {
            _profilesRepository = profilesRepository;
            _companyMapper = companyMapper;
        }

        [HttpGet]
        public IActionResult GetProfiles() { 
        
           var profiles=_profilesRepository.GetProfiles();
            if (profiles == null) return NotFound();
            return Ok(profiles);
        }

        [HttpGet("{id}")]
        public IActionResult GetProfileById(int id) { 
        
            var profile= _profilesRepository.GetProfileById(id);
            if(profile == null) return NotFound();

            var profileDto=_companyMapper.MapProfileDto(profile);
            return Ok(profileDto);

        }

        [HttpGet("username")]
        public IActionResult GetProfileByUserName(string username) { 
        
               var profile=_profilesRepository.GetProfileByUserName(username);
            if(profile == null) return NotFound();

            var profileDto= _companyMapper.MapProfileDto(profile);
            return Ok(profileDto);
        }

        [HttpGet("profilename")]
        public IActionResult GetProfileByProfileName(string profileName) { 
        
            var profile=_profilesRepository.GetProfileByProfileName(profileName);
            if(profile==null) return NotFound();

            var profileDto = _companyMapper.MapProfileDto(profile);
            return Ok(profileDto);

        }

        [HttpPost]
        public IActionResult PostProfiles([FromBody] ProfileDto profileDto)
        {
         //  if(profileDto == null) return BadRequest();
           
           var profile=_companyMapper.MapProfile(profileDto);
             _profilesRepository.AddProfile(profile);

            //  return Created(new Uri($"api/profiles/{profile.Id}"),profileDto);
            return Ok();  
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

            var updateProfil = _companyMapper.MapProfile(profileDto);
            _profilesRepository.UpdateProfile(id, updateProfil);

            return NoContent();
        }


    }
}
