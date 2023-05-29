using InstaBojan.Dtos.PostsDto;
using InstaBojan.Infrastructure.Repository.PostsRepository;
using InstaBojan.Infrastructure.Repository.ProfilesRepository;
using InstaBojan.Mappers.PostMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InstaBojan.Controllers.PostsController
{
    [Authorize(Roles = "User")]
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

            var posts = _postsRepository.GetPosts().Select(p => _postMapper.MapGetPostDto(p));


            if (posts == null) return NotFound("Posts dont't exist");

            return Ok(posts);
        }

        [HttpGet("profileName")]
        public IActionResult GetPostsByProfileName(string profileName)
        {

            var profile = _profilesRepository.GetProfileByProfileName(profileName);
            if (profile == null) return NotFound();

            var posts = _postsRepository.GetPostsByProfileName(profile.ProfileName).Select(p => _postMapper.MapGetPostDto(p));

            if (posts == null) return NotFound();

            return Ok(posts);
        }

        [HttpGet("{id}")]
        public IActionResult GetPostById(int id)
        {

            var post = _postsRepository.GetPostById(id);
            if (post == null) return NotFound();

            var postDto = _postMapper.MapGetPostDto(post);
            return Ok(postDto);

        }

        [HttpGet("userId")]
        public IActionResult GetPostByProfileId(int userId)
        {

            var post = _postsRepository.GetPostByProfileId(userId);
            if (post == null) return NotFound();

            var postDto = _postMapper.MapGetPostDto(post);
            return Ok(postDto);

        }


        [HttpPost]
        public IActionResult AddPost([FromBody] PostDto postDto)
        {

            var username = User.FindFirstValue(ClaimTypes.Name);
            if (!ModelState.IsValid) return BadRequest();

            var userProfile = _profilesRepository.GetProfileByUserName(username);
            if (userProfile == null)
            {
                return BadRequest("Profile doesn't exist");
            }


            var post = _postMapper.MapPost(postDto);
            post.ProfileId = userProfile.Id;

            _postsRepository.AddPost(post);

            return Created("api/posts" + "/" + post.Id, postDto);
        }

       

        [HttpPut("{id}")]
        public IActionResult UpdatePost(int id, [FromBody] PostDto updatePostDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var username = User.FindFirstValue(ClaimTypes.Name);
            if (username == null) return NotFound();

            var updpost = _postsRepository.GetPostById(id); //post
            if (updpost == null) return NotFound();

            var profile = _profilesRepository.GetProfileByUserName(username); // profil ulogovanog
                                                                              // var profileByPostId = _profilesRepository.GetProfileByPostId(id); // ovde je profil

            if (updpost.ProfileId != profile.Id && !User.IsInRole("Admin"))
            {

                return Forbid();
            }
          


            var post = _postMapper.MapPost(updatePostDto);
            _postsRepository.UpdatePost(id, post);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {

            var username = User.FindFirstValue(ClaimTypes.Name); // ovo je lako

            var delPost = _postsRepository.GetPostById(id); //koji bi post terebao biti obrisan
            if (delPost == null) return NotFound();


            var profile = _profilesRepository.GetProfileByUserName(username); // profil ulogovanog
            if (profile == null) return NotFound();
            // var profileByPostId = _profilesRepository.GetProfileByPostId(id); // 

            if (delPost.ProfileId != profile.Id && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            _postsRepository.DeletePost(id);
            return NoContent();
        }







    }
}
