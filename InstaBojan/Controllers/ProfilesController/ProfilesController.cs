using InstaBojan.Core.Models;
using InstaBojan.Dtos.ChangeFollowingStatusDto;
using InstaBojan.Dtos.PostsDto;
using InstaBojan.Dtos.ProfilesDto;
using InstaBojan.Infrastructure.Repository.PostsRepository;
using InstaBojan.Infrastructure.Repository.ProfilesRepository;
using InstaBojan.Infrastructure.Repository.UsersRepository;
using InstaBojan.Mappers.PostMapper;
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
        private readonly IPostsRepository _postsRepository;
        private readonly IProfileMapper _profileMapper;
        private readonly IUserRepository _userRepository;
        private readonly IPostMapper _postMapper;

        public ProfilesController(IProfilesRepository profilesRepository,
            IProfileMapper profileMapper,
            IUserRepository userRepository,
            IPostsRepository postRepository,
            IPostMapper postMapper)
        {
            _profilesRepository = profilesRepository;
            _profileMapper = profileMapper;
            _userRepository = userRepository;
            _postMapper = postMapper;
            _postsRepository = postRepository;
        }

        #region get
        [HttpGet("search")]
        public IActionResult SearchProfiles([FromQuery] string query)
        {

            var profiles=_profilesRepository.GetProfiles(query);
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

            return _profilesRepository.checkIfProfileFollowsProfile(username, followedUsername);

        }

        #endregion
        #region post
        [HttpPost]
        public IActionResult AddProfiles([FromBody] ProfileDto profileDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var username = User.FindFirstValue(ClaimTypes.Name);
            if (username == null) return NotFound();

            var user = _userRepository.GetUserByUserName(username);
            if (user == null) return NotFound();


            var existingProfile = _profilesRepository.GetProfileByProfileName(profileDto.ProfileName);
            if (existingProfile != null)
            {

                return BadRequest("Profile Name already exists");
            }


            var profile = _profileMapper.MapProfile(profileDto);
            profile.UserId = user.Id;

            _profilesRepository.AddProfile(profile);

            return Created("api/profiles" + "/" + profile.Id, profileDto);
        }

       

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

        [HttpPut("{id}")]
        public IActionResult UpdateProfiles(int id, [FromBody] ProfileDto updateProfileDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var username = User.FindFirstValue(ClaimTypes.Name);

            var profile = _profilesRepository.GetProfileById(id);

            if (profile == null)
            {
                return NotFound();
            }

            var profilByUserName = _profilesRepository.GetProfileByUserName(username);

            if (profilByUserName.Id != id && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            var profileByProfileName = _profilesRepository.GetProfileByProfileName(updateProfileDto.ProfileName);

            if (profileByProfileName != null && profileByProfileName.Id != id)
            {

                return BadRequest("Profile Name already exists;");
            }


            var updProfile = _profileMapper.MapProfile(updateProfileDto);

            _profilesRepository.UpdateProfile(id, updProfile);



            return NoContent();
        }


    }
}
