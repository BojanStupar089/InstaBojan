using Azure;
using InstaBojan.Core.Models;
using InstaBojan.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using InstaBojan.Infrastructure.Repository;
using InstaBojan.Infrastructure.Repository.ProfilesRepository;
using InstaBojan.Core.Pagination;

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

            return _context.Posts.Include(p=>p.Publisher).ThenInclude(pr=>pr.User).ToList();

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

        /*
        public List<Post> GetPostsByProfileId(int id)
        {
            var post = _context.Posts.Where(p => p.ProfileId == id).ToList();
            if (post == null) return null;

            return post;
        }

        */

        public IEnumerable<Post> GetPostsByProfileId(int id)
        {
            var post = _context.Posts.Include(p=>p.Publisher).ThenInclude(pr=>pr.User).Where(p => p.ProfileId == id).ToList();
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




        /*
        public List<Post> GetFeed(string username, int page=1, int pageSize=5)
        {

            Profile profile = _profileRepo.GetProfileByUserName(username); // profil
            List<Post> feedPost = new List<Post>(); //lista postova
            if (profile != null)
            {
                List<Profile> getFeedFrom = profile.Following; // profili koje pratim

                foreach (var prof in getFeedFrom)
                {
                    List<Post> posts = GetPostsByProfileId(prof.Id);
                    feedPost.AddRange(posts);
                }

                
                
                int skip = (page - 1) * pageSize;
                feedPost = feedPost.Skip(skip).Take(pageSize).ToList();

               

                return feedPost;

            }



            return null;

        }

        */


        /*
        
         public IEnumerable<Post> GetFeed(string username, int page=1, int pageSize=5)
         {

             Profile profile = _profileRepo.GetProfileByUserName(username); // profil
            //lista postova
             
            if (profile != null)
             {
                 IEnumerable<Profile> getFeedFrom = profile.Following;// profili koje pratim   

                List<Post> feedPosts = new List<Post>();

                 foreach (var prof in getFeedFrom)
                 {
                     IEnumerable<Post> posts = GetPostsByProfileId(prof.Id);
                    feedPosts.AddRange(posts);
                 }



                 int skip = page * pageSize;
                IEnumerable<Post> paginatedPosts = feedPosts.Skip(skip).Take(pageSize);



                 return paginatedPosts;

             }



             return Enumerable.Empty<Post>();

         }

        */



         










        /*
            public PagedList<Post> GetFeed(string username, int page, int pageSize)
            {
            var profile = _profileRepo.GetProfileByUserName(username);

            if (profile == null)
            {
                // Profile not found, return an empty paginated list
                return new PagedList<Post>(new List<Post>(), page, pageSize, 0);
            }

            var followedProfiles = profile.Following;
            var postQuery = _context.Posts
                .Where(p => followedProfiles.Contains(p.Publisher))
                .OrderByDescending(p => p.Id);

            var totalCount = postQuery.Count();

            var pagedPosts = postQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedList<Post>(pagedPosts, page, pageSize, totalCount);
        }

        */




        public Page<Post> GetPostByPublisher(Profile profile, IQueryable pageable)
        {
            throw new NotImplementedException();
        }

        public PagedList<Post> GetFeed(string username, int page = 0, int pageSize=4)
        {
            Profile profile = _profileRepo.GetProfileByUserName(username);
            
            List<Post> feedPosts = new List<Post>();

            if (profile != null)
            {
                IEnumerable<Profile> getFeedFrom = profile.Following;

                foreach (var prof in getFeedFrom)
                {
                    IEnumerable<Post> posts = GetPostsByProfileId(prof.Id);
                    feedPosts.AddRange(posts);
                }

                int totalCount = feedPosts.Count;
                int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
                int skip = page  * pageSize; 
                List<Post> paginatedPosts = feedPosts.Skip(skip).Take(pageSize).ToList();

                return new PagedList<Post>(paginatedPosts, page, totalPages, pageSize, totalCount);
            }

            return new PagedList<Post>(new List<Post>(), 1, 1, pageSize, 0);
        
    }
    }
}
