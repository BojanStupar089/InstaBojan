
using InstaBojan.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Azure;

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




        List<Post> GetFeed(string username,int page,int pageSize);

        Page<Post> GetPostByPublisher(Profile profile,IQueryable pageable);


        Page<Post> GetExplore(string username, int page, int size);
        




    }
}
