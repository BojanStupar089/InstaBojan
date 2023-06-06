using Azure;
using InstaBojan.Core.Models;
using InstaBojan.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using InstaBojan.Infrastructure.Repository;
using InstaBojan.Infrastructure.Repository.ProfilesRepository;

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


        public List<Post> GetFeed(string username, int page, int pageSize)
        {

        //    User user = _context.Users.SingleOrDefault(u => u.UserName == username);//ovo je user

            Profile profile =  _profileRepo.GetProfileByUserName(username);
            List<Post> feedPost = new List<Post>();
            if (profile != null)
            {
                List<Profile> getFeedFrom = profile.Following;
               
                foreach (var prof in getFeedFrom)
                {
                    for(int i=0; i<prof.Posts.Count; i++)
                    {
                        feedPost.Add(prof.Posts[i]);
                    }
                }

            //    profile.Following.Add(user.Id);// dobijes listu povezanu sa korisnikom

                // profiles.Add(profile);// dodas korisnikov profil direktno u listu

      //          IEnumerable<Post> posts = profiles.SelectMany(p => p.Posts).
     //               OrderByDescending(p => p.ProfileId).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }

            return feedPost ;

        }



        public Page<Post> GetPostByPublisher(Profile profile, IQueryable pageable)
        {
            throw new NotImplementedException();
        }

        public Page<Post> GetExplore(string username, int page, int size)
        {
            throw new NotImplementedException();
        }
    }
}
