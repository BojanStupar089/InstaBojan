using Google.Api;
using InstaBojan.Core.Models;
using InstaBojan.Infrastructure.Data;
using InstaBojan.Infrastructure.Repository.UsersRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBokiInsta.TestUsersRepository
{
    public class UsersRepositoryTests
    {

        private readonly DbContextOptions<InstagramStoreContext> _options;
        private readonly InstagramStoreContext _context;
        private readonly UsersRepository _userRepository;

        public UsersRepositoryTests() {

            _options = new DbContextOptionsBuilder<InstagramStoreContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            _context = new InstagramStoreContext(_options);
            _userRepository = new UsersRepository(_context);
        
        }
        
        [Fact]
        public void GetUsers_ReturnsListOfUsers() {

          /*  var options = new DbContextOptionsBuilder<InstagramStoreContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            using var context = new InstagramStoreContext(_options);*/

            _context.Users.AddRange(new List<User>
    {
        new User { UserName = "kanu4",Email="kanu4@gmail.com",Password="kanu4" },
        new User { UserName = "zidane5",Email="zidane5@gmail.com",Password="zidane5" },
        new User { UserName = "rcarlos3",Email="rcarlos3@gmail.com",Password="rcarlos3" }
    });
            _context.SaveChanges();

            var userRepository = new UsersRepository(_context);

            
            List<User> users = userRepository.GetUsers();

          
            Assert.NotNull(users);
            Assert.Equal(3, users.Count);

        }

        [Fact]

        public void AddUser_AddsUserToDatabase() {


         var options = new DbContextOptionsBuilder<InstagramStoreContext>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

         using var context = new InstagramStoreContext(options);
         var userRepository = new UsersRepository(context);

         var user = new User { UserName = "testuser",Email="testuser@gmail.com",Password="testuser" };

            // Act
          userRepository.AddUser(user);

            // Assert
         Assert.Equal(1, context.Users.Count());
         Assert.Contains(user, context.Users);

        }

        [Fact]
        public void GetUserById_ReturnsUserWhenFound() {

            // Arrange
            var testUser = new User { Id = 1, UserName = "testuser",Email="testuser@gmail.com",Password="testuser" };
            _context.Users.Add(testUser);
            _context.SaveChanges();

            // Act
            var result = _userRepository.GetUserById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testUser.Id, result.Id);
           
        }

        [Fact]
        public void GetUserByUserName_ReturnsUserWhenFound() {

            var testUser = new User { UserName = "testuser",Email ="testuser@gmail.com",Password="testuser"};
            _context.Users.Add(testUser);
            _context.SaveChanges();


            var result = _userRepository.GetUserByUserName("testuser");
            Assert.NotNull(result);
            Assert.Equal(testUser.UserName, result.UserName);
        
        }

        [Fact]
        public void DeleteUser_RemovesUserFromContextAndSaveChanges() {

         

            var testUser = new User { Id = 1, UserName = "testuser",Email="testuser@gmail.com",Password="testuser" };
            _context.Users.Add(testUser);
            _context.SaveChanges();

            // Act
            var result = _userRepository.DeleteUser(testUser.Id);

            // Assert
            Assert.True(result);
            Assert.DoesNotContain(testUser, _context.Users);

        }

        [Fact]
        public void UpdateUser_UpdateUserInContextAndSaveChanges() {

            var testUser = new User { Id = 4, UserName = "kanu",Email="kanu4@gmail.com", Password="kanu4" };
            _context.Users.Add(testUser);
            _context.SaveChanges();

            var updatedUser = new User { Id = 4, UserName = "kanu44",Email="testuser@gmail.com",Password="testuser"};

            // Act
            var userRepository = new UsersRepository(_context);
            var result =userRepository.UpdateUser(updatedUser.Id, updatedUser);

            // Assert
            Assert.True(result);
            Assert.Equal(updatedUser.UserName, testUser.UserName);

        }


       /* public void Dispose()
        {
            if(_context!=null)
            _context.Database.EnsureDeleted();
            _context.Dispose();

        }

        */

    }


   
}
