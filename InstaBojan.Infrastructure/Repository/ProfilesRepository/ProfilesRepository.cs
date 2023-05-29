using Google.Api;
using InstaBojan.Core.Models;
using InstaBojan.Infrastructure.Data;
using InstaBojan.Infrastructure.Repository.IFileStorageService;
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
        private readonly IFileStorageRepository _fileStorage;

        public ProfilesRepository(InstagramStoreContext context)
        {
            _context = context;
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
                updProfile.FirstName = profile.FirstName;
                updProfile.LastName = profile.LastName;
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

        public string UploadProfilePicture(int profileId, IFormFile file)
        {

            var profile = GetProfileById(profileId);
            if (profile == null) return null;


            var filePath = Path.Combine("C:\\Users\\Panonit\\Desktop\\pictures", file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);

            }

            profile.ProfilePicture = filePath;
            _context.SaveChanges();

            return filePath;
        }



        public bool AddPostByProfile(int profileId, IFormFile picture, string text)
        {
            var profile = GetProfileById(profileId);
            if (profile == null) return false;

            var picturePath = _fileStorage.SaveFile(picture);


            var post = new Post
            {

                Picture = picturePath,
                Text = text
            };

            profile.Posts.Add(post); // preko liste da dodas. 
            _context.SaveChanges();
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
