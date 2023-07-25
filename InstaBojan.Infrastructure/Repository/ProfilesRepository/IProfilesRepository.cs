using InstaBojan.Core.Models;

namespace InstaBojan.Infrastructure.Repository.ProfilesRepository
{
    public interface IProfilesRepository
    {

        public List<Profile> GetProfiles(string query);
        public Profile GetProfileById(int id);
        public Profile GetProfileByProfileName(string profileName);
        public Profile GetProfileByUserId(int userId);
        public Profile GetProfileByUserName(string username);
        bool CheckIfProfileFollowsProfile(string profileName, string followedProfileName);
        public bool AddProfile(Profile profile);
        public void FollowUnFollow(string profileName, string otherProfileName);
        public bool UpdateProfile(string username, Profile profile);
        public bool DeleteProfile(int id);







    }
}
