/*

using InstaBojan.Core.Models;
using InstaBojan.Infrastructure.Data;
using InstaBojan.Infrastructure.Repository.UsersRepository;
using Microsoft.EntityFrameworkCore;

namespace TestBokiInsta.TestUsersRepository
{
    public class UsersRepositoryTests
    {

        private readonly DbContextOptions<InstagramStoreContext> _options;
        private readonly InstagramStoreContext _context;
        private readonly UsersRepository _userRepository;

        public UsersRepositoryTests()
        {

            _options = new DbContextOptionsBuilder<InstagramStoreContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            _context = new InstagramStoreContext(_options);
            _userRepository = new UsersRepository(_context);

        }

        [Fact]
        public void GetUsers_ReturnsListOfUsers()
        {
            var users = new List<User> {

              new User { UserName = "kanu4",Email="kanu4@gmail.com",Password="kanu4" },
              new User { UserName = "zidane5",Email="zidane5@gmail.com",Password="zidane5" },
              new User { UserName = "rcarlos3",Email="rcarlos3@gmail.com",Password="rcarlos3" }
            };

            _context.Users.AddRange(users);
            _context.SaveChanges();

            var result = _userRepository.GetUsers();

            Assert.NotNull(users);
            Assert.Equal(result.Count, users.Count);

        }



        [Fact]
        public void GetUserById_ReturnsUserWhenFound()
        {

            // Arrange
            var testUser = new User { Id = 1, UserName = "testuser", Email = "testuser@gmail.com", Password = "testuser" };
            _context.Users.Add(testUser);
            _context.SaveChanges();

            // Act
            var result = _userRepository.GetUserById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testUser.Id, result.Id);

        }



        [Fact]
        public void GetUserByUserName_ReturnsUserWhenFound()
        {

            var testUser = new User { UserName = "testuser", Email = "testuser@gmail.com", Password = "testuser" };
            _context.Users.Add(testUser);
            _context.SaveChanges();


            var result = _userRepository.GetUserByUserName("testuser");
            Assert.NotNull(result);
            Assert.Equal(testUser.UserName, result.UserName);

        }



        [Fact]

        public void AddUser_AddsUserToDatabase()
        {

            string password = "testuser";
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User { UserName = "testuser", Email = "testuser@gmail.com", Password = hashedPassword };

            // Act
            _userRepository.AddUser(user);

            // Assert
            Assert.Equal(1, _context.Users.Count());
            Assert.Contains(user, _context.Users);
            Assert.NotNull(hashedPassword);
            Assert.True(BCrypt.Net.BCrypt.Verify(password, hashedPassword));

        }




        [Fact]
        public void UpdateUser_UpdateUserToDatabase()
        {

            string password = "testuser";
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var testUser = new User { Id = 1, UserName = "testuser", Email = "testuser@gmail.com", Password = hashedPassword };
            _context.Users.Add(testUser);
            _context.SaveChanges();

            var updatedUser = new User { Id = 1, UserName = "kanu44", Email = "testuser@gmail.com", Password = hashedPassword };

            // Act
            var userRepository = new UsersRepository(_context);
            var result = userRepository.UpdateUser(updatedUser.Id, updatedUser);

            // Assert
            Assert.True(result);
            Assert.Equal(updatedUser.UserName, testUser.UserName);
            Assert.Equal(updatedUser.Email, testUser.Email);
            /* Assert.NotNull(hashedPassword);
             Assert.True(BCrypt.Net.BCrypt.Verify(password, hashedPassword));
             Assert.Equal(updatedUser.Password, testUser.Password);*/

/*


        }



        [Fact]
        public void DeleteUser_RemoveUserFromDatabase()
        {

            var testUser = new User { Id = 1, UserName = "testuser", Email = "testuser@gmail.com", Password = "testuser" };
            _context.Users.Add(testUser);
            _context.SaveChanges();

            // Act
            var result = _userRepository.DeleteUser(testUser.Id);

            // Assert
            Assert.True(result);
            Assert.DoesNotContain(testUser, _context.Users);

        }





        public void Dispose()
        {
            if (_context != null)
                _context.Database.EnsureDeleted();
            _context.Dispose();

        }





    }



}

*/
