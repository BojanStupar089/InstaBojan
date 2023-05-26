using InstaBojan.Core.Models;
using InstaBojan.Infrastructure.Data;
using InstaBojan.Infrastructure.Repository.ProfilesRepository;
using InstaBojan.Infrastructure.Repository.UsersRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBokiInsta.TestProfiesRepository
{
    public class TestProfileRepository
    {


        private readonly DbContextOptions<InstagramStoreContext> _options;
        private readonly InstagramStoreContext _context;
        private readonly ProfilesRepository _profilesRepository;

        public TestProfileRepository()
        {

            _options = new DbContextOptionsBuilder<InstagramStoreContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            _context = new InstagramStoreContext(_options);
            _profilesRepository = new ProfilesRepository(_context);

        }

        [Fact]
        public void GetProfiles_ReturnsListOfProfiles() {

            
            var profiles = new List<Profile>
            {
                
              new Profile
              {
                Id = 1,
                FirstName = "profile1",
                LastName = "profile1",
                ProfilePicture = "profile1",
                Birthday =DateTime.Parse("2023-10-04"),
                Gender = "male",
                UserId = 1,
                Followers = new List<Profile>(),
                Following = new List<Profile>()

              },    
             

            };

            _context.Profiles.AddRange(profiles);
            _context.SaveChanges();

            var result =_profilesRepository.GetProfiles();

            Assert.NotNull(result);
            Assert.Equal(profiles.Count, result.Count);
        
        }



    }
}
