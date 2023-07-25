using InstaBojan.Core.Models;

namespace InstaBojan.Infrastructure.Repository.PostsRepository
{
    public interface IPostsRepository
    {
        List<Post> GetPosts();
        List<Post> GetPostsByProfileName(string profileName);
        Post GetPostById(int id);
        IEnumerable<Post> GetPostsByProfileId(int id);
        IEnumerable<Post> GetUserPosts(string username, int page, int pageSize);
        IEnumerable<Post> GetFeed(string username, int page, int pageSize);
        bool AddPost(Post post);
        bool UpdatePost(int id, Post post);
        bool DeletePost(int id);

        //PaginatedResult<Post> GetFeed(string username, int page, int pageSize);



    }
}
