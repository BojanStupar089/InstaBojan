using InstaBojan.Core.Models;
using InstaBojan.Infrastructure.Data;
using InstaBojan.Infrastructure.Repository.ProfilesRepository;
using Microsoft.EntityFrameworkCore;

namespace TestBokiInsta.TestRepositories.TestProfilesRepository
{
    public class TestProfilesRepository
    {

        private readonly DbContextOptions<InstagramStoreContext> _options;
        private readonly InstagramStoreContext _context;
        private readonly ProfilesRepository _profilesRepository;

        public TestProfilesRepository()
        {

            _options = new DbContextOptionsBuilder<InstagramStoreContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            _context = new InstagramStoreContext(_options);
            _profilesRepository = new ProfilesRepository(_context);

        }

        [Fact]
        public void GetProfiles_ReturnsListOfProfiles()
        {


            var profiles = new List<Profile>
            {

              new Profile
              {FirstName = "profile1",LastName = "profile1",ProfileName="profile1",ProfilePicture = "profile1",
               Birthday =DateTime.Parse("2023-10-04"),Gender = "male",UserId = 1,Followers = new List<Profile>(),Following = new List<Profile>()

              },
                new Profile
              {FirstName = "profile2",LastName = "profile2",ProfileName="profile2",ProfilePicture = "profile2",
               Birthday =DateTime.Parse("2023-10-04"),Gender = "male",UserId = 1,Followers = new List<Profile>(),Following = new List<Profile>()

              },
                new Profile
              {FirstName = "profile3",LastName = "profile3",ProfileName="profile3",ProfilePicture = "profile3",
               Birthday =DateTime.Parse("2023-10-04"),Gender = "male",UserId = 1,Followers = new List<Profile>(),Following = new List<Profile>()

              },

            };

            _context.Profiles.AddRange(profiles);
            _context.SaveChanges();

            var result = _profilesRepository.GetProfiles();

            Assert.NotNull(result);
            Assert.Equal(profiles.Count, result.Count);


        }






        [Fact]
        public void GetProfileById_ReturnsProfileWhenFound()
        {

            var profile = new Profile
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                ProfileName = "profile1",
                ProfilePicture = "profile.jpg",
                UserId = 1,
                Birthday = new DateTime(1990, 1, 1),
                Gender = "Male",
                Followers = new List<Profile>(),
                Following = new List<Profile>()
            };

            _context.Profiles.Add(profile);
            _context.SaveChanges();

            var result = _profilesRepository.GetProfileById(profile.Id);

            Assert.NotNull(result);
            Assert.Equal(profile.Id, result.Id);
            Assert.Equal(profile.FirstName, result.FirstName);
            Assert.Equal(profile.LastName, result.LastName);
            Assert.Equal(profile.ProfilePicture, result.ProfilePicture);
            Assert.Equal(profile.UserId, result.UserId);
            Assert.Equal(profile.Birthday, result.Birthday);
            Assert.Equal(profile.Gender, result.Gender);
            Assert.Equal(profile.Followers.Count, result.Followers.Count);
            Assert.Equal(profile.Following.Count, result.Following.Count);

        }

        [Fact]
        public void GetProfileByUserName_ReturnsProfileWhenFound()
        {

            var profile = new Profile
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                ProfileName = "profile1",
                ProfilePicture = "profile.jpg",
                UserId = 1,
                User = new User { UserName = "testuser", Email = "testuser@gmail.com", Password = "testuser" },
                Birthday = new DateTime(1990, 1, 1),
                Gender = "Male",
                Followers = new List<Profile>(),
                Following = new List<Profile>()
            };

            _context.Profiles.Add(profile);
            _context.SaveChanges();

            var result = _profilesRepository.GetProfileByUserName(profile.User.UserName);

            Assert.NotNull(result);
            Assert.Equal(profile.Id, result.Id);
            Assert.Equal(profile.FirstName, result.FirstName);
            Assert.Equal(profile.LastName, result.LastName);
            Assert.Equal(profile.ProfilePicture, result.ProfilePicture);
            Assert.Equal(profile.UserId, result.UserId);
            Assert.Equal(profile.Birthday, result.Birthday);
            Assert.Equal(profile.Gender, result.Gender);
            Assert.Equal(profile.Followers.Count, result.Followers.Count);
            Assert.Equal(profile.Following.Count, result.Following.Count);

        }

        [Fact]
        public void GetProfileByProfileName_ReturnsProfileWhenFound()
        {

            var profile = new Profile
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                ProfileName = "profile1",
                ProfilePicture = "profile.jpg",
                UserId = 1,
                Birthday = new DateTime(1990, 1, 1),
                Gender = "Male",
                Followers = new List<Profile>(),
                Following = new List<Profile>()
            };

            _context.Profiles.Add(profile);
            _context.SaveChanges();

            var result = _profilesRepository.GetProfileByProfileName(profile.ProfileName);

            Assert.NotNull(result);
            Assert.Equal(profile.Id, result.Id);
            Assert.Equal(profile.FirstName, result.FirstName);
            Assert.Equal(profile.LastName, result.LastName);
            Assert.Equal(profile.ProfilePicture, result.ProfilePicture);
            Assert.Equal(profile.UserId, result.UserId);
            Assert.Equal(profile.Birthday, result.Birthday);
            Assert.Equal(profile.Gender, result.Gender);
            Assert.Equal(profile.Followers.Count, result.Followers.Count);
            Assert.Equal(profile.Following.Count, result.Following.Count);

        }


        [Fact]
        public void GetProfileByUserId_ReturnsProfileWhenFound()
        {

            var profile = new Profile
            {

                FirstName = "John",
                LastName = "Doe",
                ProfileName = "profile1",
                ProfilePicture = "profile.jpg",
                User = new User { Id = 1, UserName = "testuser", Email = "testuser@gmail.com", Password = "testuser" },
                Birthday = new DateTime(1990, 1, 1),
                Gender = "Male",
                Followers = new List<Profile>(),
                Following = new List<Profile>()
            };

            _context.Profiles.Add(profile);
            _context.SaveChanges();

            var result = _profilesRepository.GetProfileByUserId(profile.User.Id);

            Assert.NotNull(result);
            Assert.Equal(profile.FirstName, result.FirstName);
            Assert.Equal(profile.LastName, result.LastName);
            Assert.Equal(profile.ProfilePicture, result.ProfilePicture);
            Assert.Equal(profile.UserId, result.UserId);
            Assert.Equal(profile.Birthday, result.Birthday);
            Assert.Equal(profile.Gender, result.Gender);
            Assert.Equal(profile.Followers.Count, result.Followers.Count);
            Assert.Equal(profile.Following.Count, result.Following.Count);
        }

        [Fact]
        public void GetProfileByPostId_ReturnsProfileWhenFound()
        {

            var profile = new Profile
            {
                FirstName = "John",
                LastName = "Doe",
                ProfileName = "testprofile",
                ProfilePicture = "testprofilepicture",
                Birthday = DateTime.Parse("2023-10-14"),
                Gender = "male",
                Posts = new List<Post>
            {
                new Post { Id = 1, Picture = "testpost", Text = "testimageurl" }
            }

            };

            _context.Profiles.Add(profile);
            _context.SaveChanges();

            var result = _profilesRepository.GetProfileByPostId(profile.Posts.First().Id);

            //Assert

            Assert.NotNull(result);
            Assert.Equal(profile.Id, result.Id);
            Assert.Equal(profile.ProfileName, result.ProfileName);
            Assert.Equal(profile.ProfilePicture, result.ProfilePicture);
        }





        [Fact]
        public void AddProfile_AddsProfileToVardse()
        {

            var profile = new Profile
            {

                FirstName = "profile1",
                LastName = "profile1",
                ProfilePicture = "profile1",
                ProfileName = "profile1",
                Birthday = DateTime.Parse("2023-10-04"),
                Gender = "male",
                UserId = 1,
                User = new User { UserName = "testuser", Email = "testuser", Password = "testuser" }
            };

            var result = _profilesRepository.AddProfile(profile);

            Assert.True(result);
            Assert.Equal(1, _context.Profiles.Count());
            Assert.Contains(profile, _context.Profiles);

        }




        [Fact]
        public void AddFollowing_AddsFollowingProfile()
        {

            var loggedInProfile = new Profile
            {
                FirstName = "John",
                LastName = "Doe",
                ProfileName = "loggedInProfile",
                ProfilePicture = "picture",
                Following = new List<Profile>()
            };

            var targetProfile = new Profile
            {
                FirstName = "Pablo",
                LastName = "Laso",
                ProfileName = "targetProfile",
                ProfilePicture = "picture",
                Followers = new List<Profile>()
            };

            _context.Profiles.Add(loggedInProfile);
            _context.Profiles.Add(targetProfile);
            _context.SaveChanges();

            // Act
            _profilesRepository.AddFollowing(loggedInProfile.Id, targetProfile.Id);

            // Assert
            var updatedLoggedInProfile = _context.Profiles
                .Include(p => p.Following)
                .FirstOrDefault(p => p.Id == loggedInProfile.Id);

            var updatedTargetProfile = _context.Profiles
                .Include(p => p.Followers)
                .FirstOrDefault(p => p.Id == targetProfile.Id);

            Assert.NotNull(updatedLoggedInProfile);
            Assert.NotNull(updatedTargetProfile);

            Assert.Contains(targetProfile, updatedLoggedInProfile.Following);
            Assert.Contains(loggedInProfile, updatedTargetProfile.Followers);
        }



        [Fact]
        public void UpdateProfile_UpdateProfileToDatabase()
        {

            var profile = new Profile
            {
                FirstName = "John",
                LastName = "Doe",
                ProfileName = "updatedProfile",
                ProfilePicture = "updatedPicture",
                Birthday = DateTime.Parse("2023-10-06"),
                Gender = "female"
            };

            _context.Profiles.Add(profile);
            _context.SaveChanges();

            var updatedProfile = new Profile
            {
                Id = 1,
                ProfileName = "newName",
                ProfilePicture = "newPicture",
                Birthday = DateTime.Parse("2023-10-06"),
                Gender = "male"
            };

            var result = _profilesRepository.UpdateProfile(updatedProfile.Id, updatedProfile);



            Assert.True(result);

            var profileFromDb = _context.Profiles.FirstOrDefault(p => p.Id == updatedProfile.Id);
            Assert.NotNull(profileFromDb);
            Assert.Equal(updatedProfile.ProfileName, profileFromDb.ProfileName);
            Assert.Equal(updatedProfile.ProfilePicture, profileFromDb.ProfilePicture);
            Assert.Equal(updatedProfile.Birthday, profileFromDb.Birthday);
            Assert.Equal(updatedProfile.Gender, profileFromDb.Gender);

        }


        [Fact]
        public void DeleteProfile_RemoveProfileFromDatabase()
        {

            var profile = new Profile
            {
                FirstName = "John",
                LastName = "Doe",
                ProfileName = "profileToDelete",
                ProfilePicture = "profilePicture",
                Birthday = DateTime.Parse("2023-10-14"),
                Gender = "male"
            };

            _context.Profiles.Add(profile);
            _context.SaveChanges();

            var result = _profilesRepository.DeleteProfile(profile.Id);

            //Assert

            Assert.True(result);
            Assert.DoesNotContain(profile, _context.Profiles);

        }


        public void Dispose()
        {
            if (_context != null)
                _context.Database.EnsureDeleted();
            _context.Dispose();

        }


    }
}
