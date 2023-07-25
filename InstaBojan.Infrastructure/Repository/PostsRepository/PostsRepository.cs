using InstaBojan.Core.Models;
using InstaBojan.Infrastructure.Data;
using InstaBojan.Infrastructure.Repository.ProfilesRepository;
using Microsoft.EntityFrameworkCore;

namespace InstaBojan.Infrastructure.Repository.PostsRepository
{
    public class PostsRepository : IPostsRepository
    {
        private readonly InstagramStoreContext _context;
        private IProfilesRepository _profileRepo;

        public PostsRepository(InstagramStoreContext context, IProfilesRepository profilesRepo)
        {
            _context = context;
            _profileRepo = profilesRepo;
        }
        #region get
        public List<Post> GetPosts()
        {

            return _context.Posts.Include(p => p.Publisher).ThenInclude(pr => pr.User).ToList();

        }

        public List<Post> GetPostsByProfileName(string profileName)
        {
            var posts = _context.Posts.Where(p => p.Publisher.ProfileName == profileName).ToList();
            return posts;
        }

        public Post GetPostById(int id)
        {
            var post = _context.Posts.Include(p => p.Publisher).ThenInclude(pr => pr.User).FirstOrDefault(p => p.Id == id);
            if (post == null) return null;

            return post;
        }


        public IEnumerable<Post> GetPostsByProfileId(int id)
        {
            var post = _context.Posts.Include(p => p.Publisher).ThenInclude(pr => pr.User).Where(p => p.ProfileId == id).ToList();
            if (post == null) return null;

            return post;
        }

        public IEnumerable<Post> GetUserPosts(string username, int page, int pageSize)
        {
            var posts = _context.Posts.Include(p => p.Publisher).ThenInclude(pr => pr.User)
                        .Where(p => p.Publisher.User.UserName == username)
                        .Skip((page - 1) * pageSize).Take(pageSize);

            return posts;

        }



        public IEnumerable<Post> GetFeed(string username, int page, int pageSize)
        {

            Profile profile = _profileRepo.GetProfileByUserName(username); // profil


            if (profile != null)
            {
                IEnumerable<Profile> getFeedFrom = profile.Following;// profili koje pratim   

                List<Post> feedPosts = new List<Post>();

                foreach (var prof in getFeedFrom)
                {
                    IEnumerable<Post> posts = GetPostsByProfileId(prof.Id);
                    feedPosts.AddRange(posts);
                }

                int totalElements = feedPosts.Count();
                int skip = (page - 1) * pageSize;
                IEnumerable<Post> paginatedPosts = feedPosts.Skip(skip).Take(pageSize);



                return paginatedPosts;

            }

            return Enumerable.Empty<Post>();

        }











        #endregion

        #region post
        public bool AddPost(Post post)
        {
            if (post == null) return false;

            _context.Posts.Add(post);
            _context.SaveChanges();
            return true;
        }

        #endregion

        #region put
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

        #endregion


        #region delete
        public bool DeletePost(int id)
        {
            var delPost = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (delPost == null) return false;

            _context.Posts.Remove(delPost);
            _context.SaveChanges();
            return true;
        }

        /*

        public PaginatedResult<Post> GetFeed(string username, int page, int pageSize)
        {

            Profile profile = _profileRepo.GetProfileByUserName(username);

            if (profile != null)
            {

                IEnumerable<Profile> getFeedFrom = profile.Following;
                List<Post> feedPosts = new List<Post>();

                foreach (var prof in getFeedFrom)
                {

                    IEnumerable<Post> posts = GetPostsByProfileId(prof.Id);
                    feedPosts.AddRange(posts);
                }


                int totalElements = feedPosts.Count;
                int skip = (page - 1) * pageSize;

                IEnumerable<Post> paginatedPosts = feedPosts.Skip(skip).Take(pageSize);


                return new PaginatedResult<Post>
                {
                    Data = paginatedPosts,
                    TotalElements = totalElements

                };

            }

            return new PaginatedResult<Post>
            {
                Data = Enumerable.Empty<Post>(),
                TotalElements = 0

            };
        }
        */


        #endregion








    }
}
