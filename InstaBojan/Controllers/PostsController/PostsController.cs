using InstaBojan.Dtos.PostsDto;
using InstaBojan.Infrastructure.Repository.PostsRepository;
using InstaBojan.Infrastructure.Repository.ProfilesRepository;
using InstaBojan.Mappers.PostMapper;
using InstaBojan.Mappers.ProfileMapper;
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
        private readonly IProfileMapper _profileMapper;

        public PostsController(IPostsRepository postsRepository, IPostMapper postMapper, IProfilesRepository profilesRepository)
        {
            _postsRepository = postsRepository;
            _postMapper = postMapper;
            _profilesRepository = profilesRepository;
        }

        [HttpGet()]
        public IActionResult GetPosts()
        {

            var posts = _postsRepository.GetPosts().Select(p => _postMapper.MapGetPostDto(p));

            if (!posts.Any())
            {
                return NotFound("No posts found");
            }

            return Ok(posts);
        }



        [HttpGet("profileName")]
        public IActionResult GetPostsByProfileName(string profileName)
        {


            var posts = _postsRepository.GetPostsByProfileName(profileName);
            if (posts == null) return NotFound();

            var postsDto = _postMapper.MapListPostDto(posts);



            return Ok(postsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetPostById(int id)
        {

            var post = _postsRepository.GetPostById(id);
            if (post == null) return NotFound();

            var postDto = _postMapper.MapGetPostDto(post);
            return Ok(postDto);

        }

        [HttpGet("profileId")]
        public IActionResult GetPostByProfileId(int profileId)
        {

            var post = _postsRepository.GetPostsByProfileId(profileId);
            if (post == null) return NotFound();

            var postDto = _postMapper.MapEnumPostDto(post);
            return Ok(postDto);

        }

        [HttpGet("user-posts")]
        public IActionResult GetUserPosts([FromQuery] string username, [FromQuery] int page, [FromQuery] int size)
        {

            // var username = User.FindFirstValue(ClaimTypes.Name);

            var posts = _postsRepository.GetUserPosts(username, page, size);
            if (posts == null) return NotFound();

            var postsDto = _postMapper.MapEnumPostDto(posts);

            return Ok(postsDto);

        }


        [HttpPost]
        public IActionResult AddPost([FromBody] NewPostDto postDto)
        {

            var username = User.FindFirstValue(ClaimTypes.Name);
            if (!ModelState.IsValid) return BadRequest();

            var userProfile = _profilesRepository.GetProfileByUserName(username);
            if (userProfile == null)
            {
                return BadRequest("Profile doesn't exist");
            }


            var post = _postMapper.MapNewPost(postDto);
            post.ProfileId = userProfile.Id;

            _postsRepository.AddPost(post);

            return Created("api/posts" + "/" + post.Id, postDto);
        }



        [HttpPut("{postId}")]
        public IActionResult UpdatePost(int postId, [FromBody] UpdatePostDto updatePostDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var username = User.FindFirstValue(ClaimTypes.Name);
            if (username == null) return NotFound();

            var updpost = _postsRepository.GetPostById(postId); //post
            if (updpost == null) return NotFound();

            var profile = _profilesRepository.GetProfileByUserName(username); // profil ulogovanog
                                                                              // var profileByPostId = _profilesRepository.GetProfileByPostId(id); // ovde je profil

            if (updpost.ProfileId != profile.Id && !User.IsInRole("Admin"))
            {

                return Forbid();
            }



            var post = _postMapper.MapUpdatePost(updatePostDto);
            _postsRepository.UpdatePost(postId, post);

            return NoContent();
        }

        [HttpDelete("{postId}")]
        public IActionResult DeletePost(int postId)
        {

            var username = User.FindFirstValue(ClaimTypes.Name); // ovo je lako

            var delPost = _postsRepository.GetPostById(postId); //koji bi post terebao biti obrisan
            if (delPost == null) return NotFound();


            var profile = _profilesRepository.GetProfileByUserName(username); // profil ulogovanog
            if (profile == null) return NotFound();
            // var profileByPostId = _profilesRepository.GetProfileByPostId(id); // 

            if (delPost.ProfileId != profile.Id && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            _postsRepository.DeletePost(postId);
            return NoContent();
        }



        [HttpGet("feed")]
        public IActionResult GetFeed([FromQuery] int page, [FromQuery] int size)
        {

            var username = User.FindFirstValue(ClaimTypes.Name);
            var feeds = _postsRepository.GetFeed(username, page, size);



            var feedsDto = _postMapper.MapEnumPostDto(feeds);
            return Ok(feedsDto);
        }






    }
}
