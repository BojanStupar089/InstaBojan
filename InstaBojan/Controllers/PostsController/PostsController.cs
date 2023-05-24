using AutoMapper;
using InstaBojan.Dtos;
using InstaBojan.Dtos.PostsDto;
using InstaBojan.Infrastructure.Repository.PostsRepository;
using InstaBojan.Infrastructure.Repository.ProfilesRepository;
using InstaBojan.Mappers.PostMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InstaBojan.Controllers.PostsController
{
    [Authorize(Roles ="User")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsRepository _postsRepository;
        private readonly IPostMapper _postMapper;
        private readonly IProfilesRepository _profilesRepository;

        public PostsController(IPostsRepository postsRepository, IPostMapper postMapper, IProfilesRepository profilesRepository)
        {
            _postsRepository = postsRepository;
            _postMapper = postMapper;
            _profilesRepository = profilesRepository;
        }

        [HttpGet]
        public IActionResult GetPosts()
        {

            var posts = _postsRepository.GetPosts().Select(p => _postMapper.MapGetPostsDto(p));
            if (posts == null) return NotFound("Posts dont't exist");

            return Ok(posts);
        }

        [HttpGet("profileName")]
        public IActionResult GetPostsByProfileName(string profileName) {

            var profile = _profilesRepository.GetProfileByProfileName(profileName);
            if (profile == null) return NotFound();

            var posts = _postsRepository.GetPostsByProfileName(profile.ProfileName).Select(p => _postMapper.MapGetPostsDto(p));

            if (posts == null ) return NotFound();

            return Ok(posts);
        }

        [HttpGet("{id}")]
        public IActionResult GetPostById(int id)
        {

            var post = _postsRepository.GetPostById(id);
            if (post == null) return NotFound();

            var postDto = _postMapper.MapGetPostsDto(post);
            return Ok(postDto);

        }

        [HttpGet("userId")]
        public IActionResult GetPostByProfileId(int userId)
        {

            var post = _postsRepository.GetPostByProfileId(userId);
            if (post == null) return NotFound();

            var postDto = _postMapper.MapGetPostsDto(post);
            return Ok(postDto);

        }


        [HttpPost]
        public IActionResult AddPost([FromBody] AddPostDto postDto)
        {


            var username = User.FindFirstValue(ClaimTypes.Name);
            if (!ModelState.IsValid) return BadRequest();

            var userProfile = _profilesRepository.GetProfileByUserName(username);
            if (userProfile == null)
            {
                return BadRequest("Profile doesn't exist");
            }

            if (postDto.ProfileId != userProfile.Id)
            {

                return BadRequest("User can't add post to other user");
            }

            var post = _postMapper.MapAddPost(postDto);
            _postsRepository.AddPost(post);

            return Created("api/posts" + "/" + post.Id, postDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePost(int id, [FromBody] UpdatePostDto updatePostDto)
        {

            var username = User.FindFirstValue(ClaimTypes.Name);
            if (!ModelState.IsValid) return BadRequest();

            var existingPost = _postsRepository.GetPostById(id);
            if (existingPost == null) return NotFound();

            var profileByUserName = _profilesRepository.GetProfileByUserName(username);
            var profileByPostId = _profilesRepository.GetProfileByPostId(id); // ovde je profil

            if (profileByUserName.ProfileName != profileByPostId.ProfileName && !User.IsInRole("Admin"))
            {

                return Forbid();
            }
            //// Check if the logged-in user is the owner of the post



            var post = _postMapper.MapUpdatePost(updatePostDto);
            _postsRepository.UpdatePost(id, post);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {

            var username = User.FindFirstValue(ClaimTypes.Name); // ovo je lako
           
            var delPost = _postsRepository.GetPostById(id);
            if (delPost == null) return NotFound();

           
            var profile = _profilesRepository.GetProfileByUserName(username);
            var profileByPostId=_profilesRepository.GetProfileByPostId(id);

            if (profile.Id != profileByPostId.Id && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            _postsRepository.DeletePost(id);
            return NoContent();
        }
    }
}
