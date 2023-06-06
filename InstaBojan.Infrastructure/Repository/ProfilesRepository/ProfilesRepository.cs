using Google.Api;
using InstaBojan.Core.Models;
using InstaBojan.Infrastructure.Data;
using InstaBojan.Infrastructure.Repository.PictureRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Infrastructure.Repository.ProfilesRepository
{
    public class ProfilesRepository : IProfilesRepository
    {

        private readonly InstagramStoreContext _context;
        private readonly IPictureRepository _pictureRepository;
       

        public ProfilesRepository(InstagramStoreContext context,IPictureRepository pictureRepository)
        {
            _context = context;
            _pictureRepository = pictureRepository;
        }

        #region get

        public List<Profile> GetProfiles()
        {
            return _context.Profiles.Include(p => p.Followers).Include(p => p.Following).ToList();
        }

        public Profile GetProfileById(int id)
        {
            
            var profile = _context.Profiles.FirstOrDefault(p => p.Id == id);
            if (profile == null) { throw new Exception("blabla"); }

            return profile;
        }

        public Profile GetProfileByUserName(string username)
        {
            var profile = _context.Profiles.Include(p => p.Followers).Include(p => p.Following).FirstOrDefault(p => p.User.UserName == username);
            if (profile == null) return null;

            return profile;

        }

        public Profile GetProfileByProfileName(string name)
        {
            var profile = _context.Profiles.Include(p => p.Followers).Include(p => p.Following).FirstOrDefault(p => p.ProfileName == name);
            if (profile == null) return null;

            return profile;
        }

        public Profile GetProfileByUserId(int userId)
        {
            var profile = _context.Profiles.Include(p => p.Followers).Include(p => p.Following).FirstOrDefault(p => p.User.Id == userId);
            if (profile == null) return null;

            return profile;
        }

        #endregion

        #region post
        public bool AddProfile(Profile profile)
        {
            if (profile == null) return false;
            _context.Profiles.Add(profile);
            _context.SaveChanges();

            return true;


        }





        #endregion

        #region delete
        public bool DeleteProfile(int id)
        {
            var delProfile = _context.Profiles.FirstOrDefault(p => p.Id == id);
            if (delProfile != null)
            {

                _context.Profiles.Remove(delProfile);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
        #endregion


        #region put
        public bool UpdateProfile(int id, Profile profile)
        {
            var updProfile = _context.Profiles.FirstOrDefault(p => p.Id == id);

            if (updProfile != null)
            {

                updProfile.ProfileName = profile.ProfileName;
                updProfile.Birthday = profile.Birthday;
                updProfile.Gender = profile.Gender;
                _context.Update(updProfile);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        #endregion

        #region ProfileProfile

        public Profile GetProfileByPostId(int id)
        {
            var profile = _context.Profiles.FirstOrDefault(p => p.Posts.Any(post => post.Id == id));
            if (profile == null) return null;

            return profile;
        }



        public void AddFollowing(int loggedInProfileId, int followingId)
        {

            var loggedInProfile = _context.Profiles.Include(p => p.Following).FirstOrDefault(p => p.Id == loggedInProfileId);

            var targetProfile = _context.Profiles.FirstOrDefault(p => p.Id == followingId);

            if (loggedInProfile != null && targetProfile != null)
            {
                loggedInProfile.Following.Add(targetProfile);
                _context.SaveChanges();
            }

        }

        public string UploadProfilePicture(int profileId, IFormFile picture)
        {

            var profile = GetProfileById(profileId);
            if (profile == null) return null;

          
           var picturePath=_pictureRepository.UploadPicture(picture);

            profile.ProfilePicture = picturePath;
            _context.SaveChanges();

            return picturePath;
        }



        public string AddPostByProfile(Post addPost, IFormFile picture)
        {
            var profile = GetProfileById(addPost.ProfileId);
            if (profile == null) return null;

            var picturePath = _pictureRepository.UploadPicture(picture);


            var post = new Post
            {
                Picture = picturePath,
                Text = addPost.Text
            };

            profile.Posts.Add(post); // preko liste da dodas. 
            _context.SaveChanges();
            return picturePath;
        }

        #endregion

        #region nisamuradio
        public List<Profile> findAllByViral(bool viral)
        {
            throw new NotImplementedException();
        }

        public void FollowUnFollow(string userName, string otherUsername)
        {
            var profile = _context.Profiles.Include(p => p.Following).FirstOrDefault(p => p.ProfileName == userName);
            var profileToChangeStatus = GetProfileByProfileName(otherUsername);

            if (profile != null && profileToChangeStatus != null)
            {
                profile.Following.Add(profileToChangeStatus);
                _context.SaveChanges();
            }
        }

        public bool checkIfProfileFollowsProfile(string profileName, string followedProfileName)
        {
            var profile = _context.Profiles.Include(p => p.Following).FirstOrDefault(p => p.ProfileName == profileName);
            var profileToChangeStatus = GetProfileByProfileName(followedProfileName);

            if (profile != null && profileToChangeStatus != null)
            {
                profile.Following.Contains(profileToChangeStatus);
                return false;
            }

            return true;
        }
    }



    #endregion



    /*
        public List<Profile> GetProfiles()
        {
            return _context.Profiles
                .Include(p => p.Posts)
                .Include(p => p.Followers)
                .Include(p => p.Following)
                .ToList();
        }

        */
}
