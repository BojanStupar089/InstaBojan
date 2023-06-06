using InstaBojan.Core.Models;
using Microsoft.AspNetCore.Http;
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
        public void AddFollowing(int loggedInProfileId,int followingId);
        public string UploadProfilePicture(int profileId, IFormFile picture);
        public string AddPostByProfile(Post post,IFormFile picture);

        List<Profile> findAllByViral(Boolean viral);

        public void FollowUnFollow(string profileName, string otherProfileName);

        bool checkIfProfileFollowsProfile(string profileName, string followedProfileName);




    }
}
