using InstaBojan.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Azure;
using InstaBojan.Core.Pagination;

namespace InstaBojan.Infrastructure.Repository.PostsRepository
{
    public interface IPostsRepository
    {
        public List<Post> GetPosts();

        List<Post> GetPostsByProfileName(string profileName);
        public Post GetPostById(int id);
        // public List<Post> GetPostsByProfileId(int id);
        public IEnumerable<Post> GetPostsByProfileId(int id);
        public bool AddPost(Post post);
        public bool UpdatePost(int id, Post post);
        public bool DeletePost(int id);




        // PagedList<Post> GetFeed(string username,int page,int pageSize);
        // List<Post> GetFeed(string username, int page, int pageSize);

        IEnumerable<Post> GetFeed(string username, int page, int pageSize);

        Page<Post> GetPostByPublisher(Profile profile,IQueryable pageable);


     
        




    }
}
