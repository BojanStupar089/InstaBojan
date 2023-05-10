using InstaBojan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Core.Repository.PostRepository
{
    public interface IPostRepository
    {
        public List<Post> GetPosts();

        public Post GetPostById(int id);

        public bool AddPost(Post post);

        public bool UpdatePost(Post post);

        public bool DeletePost(int id);

    }
}
