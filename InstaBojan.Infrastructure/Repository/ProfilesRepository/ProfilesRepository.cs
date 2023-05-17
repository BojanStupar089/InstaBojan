using InstaBojan.Core.Models;
using InstaBojan.Infrastructure.Data;
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

        public ProfilesRepository(InstagramStoreContext context)
        {
            _context = context;
        }

        #region get

        public List<Profile> GetProfiles()
        {
            return _context.Profiles.ToList();
        }

        public Profile GetProfileById(int id)
        {
            var profile= _context.Profiles.FirstOrDefault(p => p.Id == id);
            if (profile == null) { return null; }

            return profile;
        }

        public Profile GetProfileByUserName(string username)
        {
            var profile = _context.Profiles.FirstOrDefault(p => p.User.UserName == username);
            if (profile == null) return null;

            return profile;
        
        }

        public Profile GetProfileByProfileName(string name)
        {
            var profile = _context.Profiles.FirstOrDefault(p => p.ProfileName == name);
            if (profile == null) return null;

            return profile;
        }

        public Profile GetProfileByUserId(int userId)
        {
            var profile = _context.Profiles.FirstOrDefault(p => p.User.Id == userId);
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
           var delProfile=_context.Profiles.FirstOrDefault(p=>p.Id == id);
            if (delProfile != null) { 
            
                 _context.Profiles.Remove(delProfile);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
        #endregion

        
        #region put
        public bool UpdateProfile(int id,Profile profile)
        {
            var updProfile = _context.Profiles.FirstOrDefault(p => p.Id == id);

            if (updProfile != null) {

                updProfile.ProfileName = profile.ProfileName;
                updProfile.ProfilePicture = profile.ProfilePicture;
                updProfile.UserId = profile.UserId;   // Ili updProfile.UserFk=profile.UserFk
                updProfile.Birthday= profile.Birthday;
                updProfile.Posts = profile.Posts;
                updProfile.Gender = profile.Gender;
                updProfile.Followers= profile.Followers;
                updProfile.Following= profile.Following;

                _context.Update(updProfile);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        #endregion
    }
}
