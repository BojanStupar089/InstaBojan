using InstaBojan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Infrastructure.Repository.ProfilesRepository
{
    public class ProfilesRepository : IProfilesRepository
    {

        #region get

        public List<Profile> GetProfiles()
        {
            throw new NotImplementedException();
        }

        public Profile GetProfileById(int id)
        {
            throw new NotImplementedException();
        }

        public Profile GetProfileByUserName(string username)
        {
            throw new NotImplementedException();
        }

        public Profile GetProfileByName(string name)
        {
            throw new NotImplementedException();
        }

        public Profile GetProfileByUserId(int userId)
        {
            throw new NotImplementedException();
        }

           #endregion

        #region post
        public bool AddProfile(Profile profile)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region delete
        public bool DeleteProfile(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        
        #region put
        public bool UpdateProfile(Profile profile)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
