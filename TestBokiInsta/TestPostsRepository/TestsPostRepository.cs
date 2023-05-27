/*
using InstaBojan.Core.Models;
using InstaBojan.Infrastructure.Data;
using InstaBojan.Infrastructure.Repository.PostsRepository;
using Microsoft.EntityFrameworkCore;
/*
namespace TestBokiInsta.TestPostsRepository
{
    public class TestsPostRepository
    {

        private readonly DbContextOptions<InstagramStoreContext> _options;
        private readonly InstagramStoreContext _context;
        private readonly PostsRepository _postsRepository;

        public TestsPostRepository()
        {

            _options = new DbContextOptionsBuilder<InstagramStoreContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            _context = new InstagramStoreContext(_options);
            _postsRepository = new PostsRepository(_context);

        }


        [Fact]
        public void GetPosts_ReturnsListOfPosts()
        {


            var posts = new List<Post>
            {


             new Post{Id=1,Picture="post1",Text="post1",ProfileId=1 },
             new Post{Id=2,Picture="post2",Text="post2",ProfileId=1 },
            new Post{Id=3,Picture="post3",Text="post3",ProfileId=1 },

             };

            _context.Posts.AddRange(posts);
            _context.SaveChanges();

            //Act
            var result = _postsRepository.GetPosts();

            //Assert

            Assert.NotNull(result);
            Assert.Equal(posts.Count, result.Count);
            Assert.Equal(posts.Select(p => p.Id), result.Select(p => p.Id));




        }

        [Fact]
        public void GetPostById_ReturnsPostWhenFound()
        {
            var post = new Post
            {
                Id = 1,
                Picture = "post1",
                Text = "post1",
                ProfileId = 1
            };

            _context.Posts.Add(post);
            _context.SaveChanges();

            var result = _postsRepository.GetPostById(post.Id);

            Assert.NotNull(result);
            Assert.Equal(post.Id, result.Id);


        }

        [Fact]

        public void GetPostsByProfileName_ReturnsListOfPosts()
        {

            var profileName = "testuser";

            var profile = new InstaBojan.Core.Models.Profile
            {
                FirstName = "profile1",
                LastName = "profile1",
                ProfilePicture = "profile1",
                Birthday = DateTime.Parse("2023-10-04"),
                Gender = "male",
                UserId = 1,
            };

            var posts = new List<Post>
            {
                new Post { Id = 1, Picture = "post1", Text = "post1", ProfileId = 1 },
                new Post { Id = 2, Picture = "post2", Text = "post2", ProfileId = 1 },

            };

            _context.Profiles.Add(profile);
            _context.Posts.AddRange(posts);
            _context.SaveChanges();

            // Act
            var result = _postsRepository.GetPostsByProfileName(profileName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(posts.Count, result.Count);
            Assert.All(result, post => Assert.Equal(profileName, post.Publisher.ProfileName));

        }

        [Fact]
        public void GetPostByProfileId_ReturnsPostWhenFound()
        {

            var post = new Post { Id = 1, Picture = "post1", Text = "post1", ProfileId = 1 };
            _context.Posts.Add(post);
            _context.SaveChanges();

            // Act
            var result = _postsRepository.GetPostByProfileId(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(post.Id, result.Id);
            Assert.Equal(post.ProfileId, result.ProfileId);
        }

        [Fact]
        public void AddPost_ReturnsTrueWhenPostAddedSuccessfully()
        {
            var post = new Post
            {
                Id = 1,
                Picture = "post1",
                Text = "post1",
                ProfileId = 1
                // Set other post properties as needed
            };

            // Act
            var result = _postsRepository.AddPost(post);

            // Assert
            Assert.True(result);
            Assert.Contains(post, _context.Posts);


        }



        [Fact]
        public void UpdatePost_ShouldUpdatePostInDatabase()
        {

            var post = new Post { Id = 1, };
            _context.Posts.Add(post);
            _context.SaveChanges();

            // Create an updated post with modified properties
            var updatedPost = new Post { Id = 1, Picture = "newPicture.jpg", Text = "Updated text" };

            // Act
            var result = _postsRepository.UpdatePost(post.Id, updatedPost);

            // Assert
            Assert.True(result);
            Assert.Equal(updatedPost.Picture, _context.Posts.Find(post.Id).Picture);
            Assert.Equal(updatedPost.Text, _context.Posts.Find(post.Id).Text);


        }

        [Fact]
        public void DeletePost_ShouldRemovePostFromDatabase()
        {

            var post = new Post { Id = 1,Picture="post1",Text="blabla",ProfileId=1 };
            _context.Posts.Add(post);
            _context.SaveChanges();

            // Act
            var result = _postsRepository.DeletePost(post.Id);

            // Assert
            Assert.True(result);
            Assert.Null(_context.Posts.Find(post.Id));
        }
    }
}

*/
