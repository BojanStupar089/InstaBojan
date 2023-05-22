using AutoMapper;
using InstaBojan.Dtos;
using InstaBojan.Dtos.PostsDto;
using InstaBojan.Infrastructure.Repository.PostsRepository;
using InstaBojan.Mappers.PostMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaBojan.Controllers.PostsController
{
   //[Authorize(Roles ="User")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsRepository _postsRepository;
        private readonly IPostMapper _postMapper;

        public PostsController(IPostsRepository postsRepository,IPostMapper postMapper)
        {
            _postsRepository = postsRepository;
            _postMapper = postMapper;
        }

        [HttpGet]
        public IActionResult GetPosts() {

           var posts = _postsRepository.GetPosts().Select(p=>_postMapper.MapPostDto(p));
            if (posts == null) return NotFound("Posts dont't exist");
           
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public IActionResult GetPostById(int id) { 
        
           var post= _postsRepository.GetPostById(id);
            if (post == null) return NotFound();

            var postDto = _postMapper.MapPostDto(post);
            return Ok(postDto);

        }


        [HttpPost]
        public IActionResult AddPost([FromBody] AddPostDto postDto) {

            if (!ModelState.IsValid) return BadRequest();
              
            var post=_postMapper.MapPost(postDto);
            _postsRepository.AddPost(post);

            return Created("api/posts" + "/" + post.Id, postDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePost(int id, [FromBody] UpdatePostDto postDto) {

             var updPost=_postsRepository.GetPostById(id);
            if (updPost == null) return NotFound();

            var post=_postMapper.MapUpdatePost(postDto);
            _postsRepository.UpdatePost(id,post);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id) {

            var delPost = _postsRepository.GetPostById(id);
            if (delPost == null) return NotFound();

            _postsRepository.DeletePost(id);
            return NoContent();
        }
    }
}
