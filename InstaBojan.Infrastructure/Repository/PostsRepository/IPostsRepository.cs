using InstaBojan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Infrastructure.Repository.PostsRepository
{
    public interface IPostsRepository
    {
        public List<Post> GetPosts();
        public Post GetPostById(int id);
        public bool AddPost(Post post);
        public bool UpdatePost(int id, Post post);
        public bool DeletePost(int id);


        
    }
}
