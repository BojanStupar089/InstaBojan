using InstaBojan.Core.Models;
using InstaBojan.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Infrastructure.Repository.PostsRepository
{
    public class PostsRepository : IPostsRepository
    {
        private readonly InstagramStoreContext _context;
     
        public PostsRepository(InstagramStoreContext context)
        {
            _context = context;
        }

        public List<Post> GetPosts()
        {

             return _context.Posts.ToList();
        }

        public Post GetPostById(int id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null) return null;

            return post;
        }

        public Post GetPostByProfileId(int id)
        {
            var post=_context.Posts.FirstOrDefault(p=>p.ProfileId == id);
            if (post == null) return null;

            return post;
        }

        public bool AddPost(Post post)
        {
            if (post == null) return false;

            _context.Posts.Add(post);
            _context.SaveChanges();
            return true;
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
          var updPost= _context.Posts.FirstOrDefault(p=>p.Id == id);
            if (updPost == null) return false;

            updPost.Picture= post.Picture;
            updPost.Text= post.Text;

            _context.Update(updPost);
            _context.SaveChanges();
            
            return true;

        }

       
    }
}
