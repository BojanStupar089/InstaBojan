using InstaBojan.Core.Models;
using Microsoft.AspNetCore.Http;
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

        List<Post> GetPostsByProfileName(string profileName);
        public Post GetPostById(int id);
        public Post GetPostByProfileId(int id);
        public bool AddPost(Post post);
        public bool UpdatePost(int id, Post post);
        public bool DeletePost(int id);


      
        
    }
}
