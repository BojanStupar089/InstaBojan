using InstaBojan.Core.Models;
using InstaBojan.Infrastructure.Data;
using InstaBojan.Infrastructure.Repository.UsersRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBokiInsta.TestUsersRepository
{
    public class UsersRepositoryTests
    {
        private readonly UsersRepository _usersRepository;
        private readonly InstagramStoreContext _context;

        public UsersRepositoryTests(UsersRepository usersRepository,InstagramStoreContext instagramStoreContext) { 
        
             
        }

        [Fact]
        public void GetUsers_ReturnsListOfUsers() {

          

            //using var context=new InstagramStoreContext(options);

            context.Users.AddRange(new List<User>
            { 
            
              new User { UserName="kanu4"},
              new User {UserName="zidane5"},
            
            });

            context.SaveChanges();

            var UsersRepository = new UsersRepository(context);

            List<User>users=_usersRepository.GetUsers();

            Assert.NotNull(users);
            Assert.Equal(2, users.Count);
        }

        [Fact]
        public void GetUserById_ReturnsUserIfExists() {

            int userId = 5;

            User user= _usersRepository.GetUserById(userId);

            Assert.NotNull(user);
            Assert.Equal(userId, user.Id);
        }

        [Fact]
        public void GetUserByUserName_ReturnsUserIfExists() {

            string testUsername = "kanu4";

            User user=_usersRepository.GetUserByUserName(testUsername);

            Assert.NotNull(user);
            Assert.Equal(testUsername, user.UserName);
        
        }

        [Fact]
        public void AddUser_ReturnsTrueIfSuccesfullyAdded() {

            User testUser = new User
            {
              UserName="kanu4",
              Email="kanu4@gmail.com",
              Password="kanu4"
            };

            bool result=_usersRepository.AddUser(testUser);

            Assert.True(result);
         //   Assert.Contains(testUser, context.Users);
        
        }

    }
}
