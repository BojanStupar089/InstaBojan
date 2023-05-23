using InstaBojan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Infrastructure.Repository.ProfilesRepository
{
    public  interface IProfilesRepository
    {

        public List<Profile> GetProfiles();
        public Profile GetProfileById(int id);
        public Profile GetProfileByProfileName(string profileName);
        public Profile GetProfileByUserId(int userId);
        public Profile GetProfileByUserName(string username);
        public bool AddProfile(Profile profile);
        public bool UpdateProfile(int id,Profile profile);
        public bool DeleteProfile(int id);

        public Profile GetProfileByPostId(int id);


        //moras napraviti metodu Follow i UnFollow

        //public Profile Follow(int id);
        //public Profile Follow(string username); // ovo sam nasao
        //


    
    }
}
