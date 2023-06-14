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

        public List<Profile> GetProfiles(string query)
        {
            return _context.Profiles.Include(u=>u.User).Include(p => p.Followers).Include(p => p.Following).Include(p => p.Posts).Where(p=>EF.Functions.Like(p.User.UserName,$"%{query}%")).ToList();
        }

        public Profile GetProfileById(int id)
        {
            
            var profile = _context.Profiles.Include(p=>p.Followers).Include(p=>p.Following).Include(p=>p.Posts).FirstOrDefault(p => p.Id == id);
            if (profile == null) { throw new Exception("blabla"); }

            return profile;
        }

        public Profile GetProfileByUserName(string username)
        {
            var profile = _context.Profiles.Include(u=>u.User).Include(p => p.Posts).Include(p => p.Followers).Include(p => p.Following).FirstOrDefault(p => p.User.UserName == username);
            if (profile == null) return null;

            return profile;

        }

        public Profile GetProfileByProfileName(string name)
        {
            var profile = _context.Profiles.Include(p=>p.Posts).Include(p => p.Followers).Include(p => p.Following).FirstOrDefault(p => p.ProfileName == name);
            if (profile == null) return null;

            return profile;
        }

        public Profile GetProfileByUserId(int userId)
        {
            var profile = _context.Profiles.Include(p=>p.Posts).Include(p => p.Followers).Include(p => p.Following).FirstOrDefault(p => p.User.Id == userId);
            if (profile == null) return null;

            return profile;
        }

        public bool checkIfProfileFollowsProfile(string userName, string followedProfile)
        {
            var profile = GetProfileByUserName(userName);
            var profileToFollow = GetProfileByUserName(followedProfile);

            return profile.Following.Contains(profileToFollow);
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

        public void FollowUnFollow(string userName, string otherUsername)
        {
            var profile = _context.Profiles.Include(p => p.Following).FirstOrDefault(p => p.User.UserName == userName);
            var profileToChangeStatus = GetProfileByUserName(otherUsername);

            if (profile != null && profileToChangeStatus != null)
            {
                if (profile.Following.Contains(profileToChangeStatus))
                {
                    profile.Following.Remove(profileToChangeStatus);
                }
                else
                {
                    profile.Following.Add(profileToChangeStatus);
                }

                _context.SaveChanges();

            }
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

       

  }
}
