using AutoMapper;
using InstaBojan.Dtos;
using InstaBojan.Helpers;
using InstaBojan.Infrastructure.Repository.PostsRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaBojan.Controllers.PostsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsRepository _postsRepository;
        private readonly ICompanyMapper _companyMapper;

        public PostsController(IPostsRepository postsRepository,ICompanyMapper companyMapper)
        {
            _postsRepository = postsRepository;
            _companyMapper = companyMapper;
        }

        [HttpGet]
        public IActionResult GetPosts() {

           var posts = _postsRepository.GetPosts();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public IActionResult GetPostById(int id) { 
        
           var post= _postsRepository.GetPostById(id);
            if (post == null) return NotFound();

            var postDto = _companyMapper.MapPostDto(post);
            return Ok(postDto);

        }


        [HttpPost]
        public IActionResult AddPost([FromBody] PostDto postDto) {

            if (postDto == null) return BadRequest();
              
            var post=_companyMapper.MapPost(postDto);
            _postsRepository.AddPost(post);

            return Created(new Uri($"api/posts/{post.Id}"), postDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePost(int id, [FromBody] PostDto postDto) {

             var updPost=_postsRepository.GetPostById(id);
            if (updPost == null) return NotFound();

            var post=_companyMapper.MapPost(postDto);
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
