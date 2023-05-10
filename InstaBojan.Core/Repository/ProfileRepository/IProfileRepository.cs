using InstaBojan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Core.Repository.ProfileRepository
{
    public interface IProfileRepository
    {
        public List<Profile> GetProfiles();

        public Profile GetProfileById(int id);

        public bool AddProfile(Profile profile);

        public bool UpdateProfile(Profile profile);

        public bool DeleteProfile(int id);
    }
}
