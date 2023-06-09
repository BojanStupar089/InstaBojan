﻿using InstaBojan.Core.Models;
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

        public List<Profile> GetProfiles(string query);
        public Profile GetProfileById(int id);
        public Profile GetProfileByProfileName(string profileName);
        public Profile GetProfileByUserId(int userId);
        public Profile GetProfileByUserName(string username);
        public bool AddProfile(Profile profile);
        public bool UpdateProfile(string username,Profile profile);
        public bool DeleteProfile(int id);
        public void FollowUnFollow(string profileName, string otherProfileName);

        bool checkIfProfileFollowsProfile(string profileName, string followedProfileName);




    }
}
