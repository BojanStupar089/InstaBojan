using InstaBojan.Controllers.PostsController;
using InstaBojan.Core.Models;
using InstaBojan.Dtos.PostsDto;
using InstaBojan.Infrastructure.Data;
using InstaBojan.Infrastructure.Repository.PostsRepository;
using InstaBojan.Infrastructure.Repository.ProfilesRepository;
using InstaBojan.Mappers.PostMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;

namespace TestBokiInsta.TestControllers.TestPostsController
{
    public class TestPostController
    {

        private DbContextOptions<InstagramStoreContext> _options;
        private InstagramStoreContext _context;
        private Mock<IPostsRepository> _postsRepositoryMock;
        private Mock<IPostMapper> _postMapperMock;
        private PostsController _postsControllerMock;
        private Mock<IProfilesRepository> _profilesRepositoryMock;
        // private Mock<ITokenBlackListWrapper> _tokenBlackListWrapperMock;


        public TestPostController()
        {

            _options = new DbContextOptionsBuilder<InstagramStoreContext>()
           .UseInMemoryDatabase(databaseName: "TestDatabase")
           .Options;

            _context = new InstagramStoreContext(_options);
            _postsRepositoryMock = new Mock<IPostsRepository>();
            _postMapperMock = new Mock<IPostMapper>();
            _profilesRepositoryMock = new Mock<IProfilesRepository>();
            _postsControllerMock = new PostsController(_postsRepositoryMock.Object, _postMapperMock.Object, _profilesRepositoryMock.Object);

        }

        [Fact]
        public void GetPosts_ReturnsOkResultWithPosts()
        {
            // Arrange
            var posts = new List<Post>
             {
                 new Post { Picture = "picture1.jpg", Text = "Post 1" },
                 new Post { Picture = "picture2.jpg", Text = "Post 2" }
              };

            _postsRepositoryMock.Setup(repo => repo.GetPosts()).Returns(posts);
            _postMapperMock
                .Setup(mapper => mapper.MapGetPostDto(It.IsAny<Post>()))
                .Returns<Post>(p => new PostDto { Picture = p.Picture, Text = p.Text });

            // Act
            var result = _postsControllerMock.GetPosts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedPosts = Assert.IsAssignableFrom<IEnumerable<PostDto>>(okResult.Value);

            Assert.Equal(posts.Count, returnedPosts.Count());
            Assert.All(returnedPosts, dto =>
            {
                var correspondingPost = posts.FirstOrDefault(p => p.Picture == dto.Picture && p.Text == dto.Text);
                Assert.NotNull(correspondingPost);
            });
        }

        [Fact]
        public void GetPostById_WithNonExistentId_ReturnsNotFoundResult()
        {
            // Arrange
            int postId = 1;
            _postsRepositoryMock.Setup(repo => repo.GetPostById(postId)).Returns((Post)null);

            // Act
            var result = _postsControllerMock.GetPostById(postId);

            // Assert
            var actualType = result.GetType();
            Assert.Equal(typeof(NotFoundResult), actualType);
        }

        [Fact]
        public void GetPostById_WithExistingId_ReturnsOkResultWithPostDto()
        {
            // Arrange
            int postId = 1;
            var post = new Post { Id = postId, Picture = "picture.jpg", Text = "Post" };
            _postsRepositoryMock.Setup(repo => repo.GetPostById(postId)).Returns(post);
            _postMapperMock.Setup(mapper => mapper.MapGetPostDto(post)).Returns(new PostDto { Picture = post.Picture, Text = post.Text });

            // Act
            var result = _postsControllerMock.GetPostById(postId);

            // Assert
            var actualType = result.GetType();
            Assert.Equal(typeof(OkObjectResult), actualType);

            var okResult = (OkObjectResult)result;
            var returnedPostDto = (PostDto)okResult.Value;

            Assert.Equal(post.Picture, returnedPostDto.Picture);
            Assert.Equal(post.Text, returnedPostDto.Text);
        }

        [Fact]
        public void GetPostByProfileId_WithExistingId_ReturnsOkResultWithPostDto()
        {
            // Arrange
            int userId = 1;
            var post = new Post { Id = 1, ProfileId = userId, Picture = "picture.jpg", Text = "Post" };
            _postsRepositoryMock.Setup(repo => repo.GetPostByProfileId(userId)).Returns(post);
            _postMapperMock.Setup(mapper => mapper.MapGetPostDto(post)).Returns(new PostDto { Picture = post.Picture, Text = post.Text });

            // Act
            var result = _postsControllerMock.GetPostByProfileId(userId);

            // Assert
            var actualType = result.GetType();
            Assert.Equal(typeof(OkObjectResult), actualType);

            var okResult = (OkObjectResult)result;
            var returnedPostDto = (PostDto)okResult.Value;

            Assert.Equal(post.Picture, returnedPostDto.Picture);
            Assert.Equal(post.Text, returnedPostDto.Text);
        }

        [Fact]
        public void GetPostByProfileId_WithNonExistentId_ReturnsNotFoundResult()
        {
            // Arrange
            int userId = 1;
            _postsRepositoryMock.Setup(repo => repo.GetPostByProfileId(userId)).Returns((Post)null);

            // Act
            var result = _postsControllerMock.GetPostByProfileId(userId);

            // Assert
            var actualType = result.GetType();
            Assert.Equal(typeof(NotFoundResult), actualType);
        }


        [Fact]
        public void GetPostsByProfileName_WithExistingProfileName_ReturnsOkResultWithPostDtos()
        {
            // Arrange
            string profileName = "john";
            var profile = new Profile { Id = 1, ProfileName = profileName };
            var posts = new List<Post>
    {
        new Post { Id = 1, ProfileId = profile.Id, Picture = "picture1.jpg", Text = "Post 1" },
        new Post { Id = 2, ProfileId = profile.Id, Picture = "picture2.jpg", Text = "Post 2" }
    };

            _profilesRepositoryMock.Setup(repo => repo.GetProfileByProfileName(profileName)).Returns(profile);
            _postsRepositoryMock.Setup(repo => repo.GetPostsByProfileName(profile.ProfileName)).Returns(posts);
            _postMapperMock
                .Setup(mapper => mapper.MapGetPostDto(It.IsAny<Post>()))
                .Returns<Post>(p => new PostDto { Picture = p.Picture, Text = p.Text });

            // Act
            var result = _postsControllerMock.GetPostsByProfileName(profileName);

            // Assert
            var actualType = result.GetType();
            Assert.Equal(typeof(OkObjectResult), actualType);

            var okResult = (OkObjectResult)result;
            var returnedPosts = (IEnumerable<PostDto>)okResult.Value;

            Assert.Equal(posts.Count, returnedPosts.Count());
            Assert.All(returnedPosts, dto =>
            {
                var correspondingPost = posts.FirstOrDefault(p => p.Picture == dto.Picture && p.Text == dto.Text);
                Assert.NotNull(correspondingPost);
            });
        }

        [Fact]
        public void GetPostsByProfileName_WithNonExistentProfileName_ReturnsNotFoundResult()
        {
            // Arrange
            string profileName = "john";
            _profilesRepositoryMock.Setup(repo => repo.GetProfileByProfileName(profileName)).Returns((Profile)null);

            // Act
            var result = _postsControllerMock.GetPostsByProfileName(profileName);

            // Assert
            var actualType = result.GetType();
            Assert.Equal(typeof(NotFoundResult), actualType);
        }


        [Fact]
        public void AddPost_ValidPostDto_ReturnsCreatedResultWithPostDto()
        {
            // Arrange
            var postDto = new PostDto { Picture = "picture.jpg", Text = "New post" };
            var username = "john";
            var profile = new Profile { FirstName = "ale", LastName = "ale", ProfilePicture = "ole", ProfileName = "profile1", User = new User { UserName = username, Email = "john@gmail.com", Password = "ole" } };

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, username) // Add any additional claims as needed
    };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);

            _postsControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = principal
                }
            };

            _profilesRepositoryMock.Setup(repo => repo.GetProfileByUserName(username)).Returns(profile);
            _postMapperMock
                .Setup(mapper => mapper.MapPost(It.IsAny<PostDto>()))
                .Returns<PostDto>(dto => new Post { Picture = dto.Picture, Text = dto.Text });

            // Act
            var result = _postsControllerMock.AddPost(postDto);

            // Assert
            var actualType = result.GetType();
            Assert.Equal(typeof(CreatedResult), actualType);

            var createdResult = (CreatedResult)result;
            var returnedPostDto = (PostDto)createdResult.Value;

            Assert.Equal(postDto.Picture, returnedPostDto.Picture);
            Assert.Equal(postDto.Text, returnedPostDto.Text);
        }



        [Fact]
        public void UpdatePost_WhenAllGood_ReturnsNoContent()
        {
            // Arrange
            int postId = 1;
            var updatePostDto = new PostDto { Picture = "newpicture.jpg", Text = "Updated post" };
            var existingPost = new Post { Id = postId, Picture = "oldpicture.jpg", Text = "Old post" };
            var profileByUserName = new Profile { ProfileName = "john", ProfilePicture = "bla", FirstName = "bla", LastName = "bla", User = new User { UserName = "john", Email = "john@gmail.com", Password = "john" } };

            var posts = new List<Post>
            {
                new Post{ Id=1,Picture="bla",Text="bla"},
                new Post{ Id=2,Picture="bla",Text="bla"}
            };

            var profileByPostId = new Profile { ProfileName = "john", ProfilePicture = "bla", FirstName = "bla", LastName = "bla", User = new User { UserName = "john", Email = "john@gmail.com", Password = "john" }, Posts = posts };

            _postsRepositoryMock.Setup(repo => repo.GetPostById(postId)).Returns(existingPost);
            _profilesRepositoryMock.Setup(repo => repo.GetProfileByUserName("john")).Returns(profileByUserName);
            _profilesRepositoryMock.Setup(repo => repo.GetProfileByPostId(postId)).Returns(profileByPostId);
            _postMapperMock.Setup(mapper => mapper.MapPost(updatePostDto)).Returns(new Post { Picture = updatePostDto.Picture, Text = updatePostDto.Text });

            // Act
            var result = _postsControllerMock.UpdatePost(postId, updatePostDto);

            // Assert
            var actualType = result.GetType();
            Assert.Equal(typeof(NoContentResult), actualType);
        }



    }






}
