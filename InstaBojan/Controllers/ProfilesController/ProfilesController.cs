using InstaBojan.Dtos.ChangeFollowingStatusDto;
using InstaBojan.Dtos.ProfilesDto;
using InstaBojan.Infrastructure.Repository.ProfilesRepository;
using InstaBojan.Mappers.ProfileMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InstaBojan.Controllers.ProfilesController
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfilesRepository _profilesRepository;
        private readonly IProfileMapper _profileMapper;



        public ProfilesController(IProfilesRepository profilesRepository,
            IProfileMapper profileMapper)
        {
            _profilesRepository = profilesRepository;
            _profileMapper = profileMapper;

        }

        #region get
        [HttpGet("search")]
        public IActionResult SearchProfiles([FromQuery] string query)
        {

            var profiles = _profilesRepository.GetProfiles(query);
            if (profiles == null)
            {
                return NotFound();
            }

            var profilesDto = _profileMapper.MapUserSearchResultDto(profiles);
            return Ok(profilesDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetProfileById(int id)
        {

            var profile = _profilesRepository.GetProfileById(id);
            if (profile == null) return NotFound();

            var profileDto = _profileMapper.MapGetProfilesDto(profile);
            return Ok(profileDto);

        }

        [HttpGet("userId")]
        public IActionResult GetProfileByUserId(int userId)
        {

            var profile = _profilesRepository.GetProfileByUserId(userId);
            if (profile == null) return NotFound();

            var profileDto = _profileMapper.MapGetProfilesDto(profile);
            return Ok(profileDto);
        }

        [HttpGet("username")]
        public IActionResult GetProfileByUserName(string username)
        {

            var profile = _profilesRepository.GetProfileByUserName(username);
            if (profile == null) return NotFound();

            var profileDto = _profileMapper.MapGetProfilesDto(profile);
            return Ok(profileDto);
        }

        [HttpGet("profilename")]
        public IActionResult GetProfileByProfileName(string profileName)
        {

            var profile = _profilesRepository.GetProfileByProfileName(profileName);
            if (profile == null) return NotFound();

            var profileDto = _profileMapper.MapGetProfilesDto(profile);
            return Ok(profileDto);

        }

        [HttpGet("follow-check")]
        public bool CheckIfUserFollowsUser([FromQuery] string username, [FromQuery] string followedUsername)
        {

            return _profilesRepository.CheckIfProfileFollowsProfile(username, followedUsername);

        }

        #endregion
        #region post



        [HttpPost("follow-unfollow")]
        public IActionResult FollowUnfollow([FromBody] ChangeFollowingStatusDto dto)
        {
            var profile = _profilesRepository.GetProfileByUserName(dto.MyUsername);
            if (profile == null) return NotFound();

            var profileChangeStatus = _profilesRepository.GetProfileByUserName(dto.OtherUsername);
            if (profileChangeStatus == null) return NotFound();

            _profilesRepository.FollowUnFollow(profile.User.UserName, profileChangeStatus.User.UserName);
            return Ok();

        }




        #endregion
        #region delete

        [HttpDelete("{id}")]
        public IActionResult DeleteProfiles(int id)
        {

            var username = User.FindFirstValue(ClaimTypes.Name);

            var delProfile = _profilesRepository.GetProfileById(id);
            if (delProfile == null) return NotFound();

            var profile = _profilesRepository.GetProfileByUserName(username);

            if (profile.Id != id && !User.IsInRole("Admin"))
            {
                return Forbid();
            }
            _profilesRepository.DeleteProfile(id);
            return NoContent();
        }

        #endregion

        #region put
        [HttpPut("{userName}")]
        public IActionResult UpdateProfiles(string userName, [FromBody] UpdateProfileDto updateProfileDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var profile = _profilesRepository.GetProfileByUserName(userName);

            if (profile == null)
            {
                return NotFound();
            }

            var profilByUserName = _profilesRepository.GetProfileByUserName(userName);

            if (profilByUserName.User.UserName != userName && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            var profileByProfileName = _profilesRepository.GetProfileByProfileName(updateProfileDto.ProfileName);

            if (profileByProfileName != null && profileByProfileName.User.UserName != userName)
            {

                return BadRequest("Profile Name already exists;");
            }


            var updProfile = _profileMapper.MapUpdateProfile(updateProfileDto);

            _profilesRepository.UpdateProfile(userName, updProfile);



            return NoContent();
        }

        #endregion
    }
}
