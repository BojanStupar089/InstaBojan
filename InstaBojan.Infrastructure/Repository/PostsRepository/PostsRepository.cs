using InstaBojan.Core.Models;
using InstaBojan.Infrastructure.Data;
using Microsoft.AspNetCore.Http;

namespace InstaBojan.Infrastructure.Repository.PostsRepository
{
    public class PostsRepository : IPostsRepository
    {
        private readonly InstagramStoreContext _context;

        public PostsRepository(InstagramStoreContext context)
        {
            _context = context;
        }
        #region get
        public List<Post> GetPosts()
        {

            return _context.Posts.ToList();
        }

        public List<Post> GetPostsByProfileName(string profileName)
        {
            var posts = _context.Posts.Where(p => p.Publisher.ProfileName == profileName).ToList();
            return posts;
        }

        public Post GetPostById(int id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null) return null;

            return post;
        }

        public Post GetPostByProfileId(int id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.ProfileId == id);
            if (post == null) return null;

            return post;
        }

        #endregion

        public bool AddPost(Post post)
        {
            if (post == null) return false;

            _context.Posts.Add(post);
            _context.SaveChanges();
            return true;
        }

        public string UploadPostPicture(int postId, IFormFile file)
        {

            var profile = GetPostById(postId);
            if (profile == null) return null;


            var filePath = Path.Combine("C:\\Users\\Panonit\\Desktop\\pictures", file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);

            }

            profile.Picture = filePath;
            _context.SaveChanges();

            return filePath;
        }

        public bool DeletePost(int id)
        {
            var delPost = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (delPost == null) return false;

            _context.Posts.Remove(delPost);
            _context.SaveChanges();
            return true;
        }



        public bool UpdatePost(int id, Post post)
        {
            var updPost = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (updPost == null) return false;

            updPost.Picture = post.Picture;
            updPost.Text = post.Text;

            _context.Update(updPost);
            _context.SaveChanges();

            return true;

        }







    }
}
