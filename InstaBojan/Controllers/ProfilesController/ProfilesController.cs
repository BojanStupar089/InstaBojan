﻿using InstaBojan.Core.Models;
using InstaBojan.Dtos;
using InstaBojan.Dtos.ProfilesDto;
using InstaBojan.Infrastructure.Repository.ProfilesRepository;
using InstaBojan.Mappers.ProfileMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

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
        public IActionResult AddProfiles([FromBody] AddProfileDto profileDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var existingProfile = _profilesRepository.GetProfileByProfileName(profileDto.ProfileName);
            if (existingProfile != null) {

                return BadRequest("Profile Name already exists");
            }

            var existingUserProfile = _profilesRepository.GetProfileByUserId(profileDto.UserId);
            if (existingUserProfile != null) {

                return BadRequest("User already has a profile");
            }
           
           var profile=_profileMapper.MapAddProfile(profileDto);
             _profilesRepository.AddProfile(profile);

           return Created("api/profiles"+"/"+profile.Id, profileDto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProfiles(int id) {

            var username = User.FindFirstValue(ClaimTypes.Name);
           
            var delProfile=_profilesRepository.GetProfileById(id);
            if(delProfile == null) return NotFound();

            var profile=_profilesRepository.GetProfileByUserName(username);
           
            if (profile.Id!=id && !User.IsInRole("Admin")) {
                return Forbid();   
            }
            _profilesRepository.DeleteProfile(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProfiles(int id,[FromBody] UpdateProfileDto updateProfileDto) {


            var username =User.FindFirstValue(ClaimTypes.Name);
          
            var profile=_profilesRepository.GetProfileById(id);

            if (profile == null) {
                return NotFound();
            }

            var profilByUserName=_profilesRepository.GetProfileByUserName(username);

            if(profilByUserName.Id !=id && !User.IsInRole("Admin")) {
                return Forbid();
            }
            var profileByProfileName=_profilesRepository.GetProfileByProfileName(updateProfileDto.ProfileName);
            if (profileByProfileName !=null  && profileByProfileName.Id != id) {

                return BadRequest("Profile Name already exists;");
            }
           

            var updProfile = _profileMapper.MapUpdateProfile(updateProfileDto);

            _profilesRepository.UpdateProfile(id, updProfile);

            return NoContent();
        }


    }
}
