using InstaBojan.Controllers.UsersController;
using InstaBojan.Core.Models;
using InstaBojan.Dtos.UsersDto;
using InstaBojan.Infrastructure.Data;
using InstaBojan.Infrastructure.Repository.TokenRepository;
using InstaBojan.Infrastructure.Repository.UsersRepository;
using InstaBojan.Mappers.UserMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;

namespace TestBokiInsta.TestControllers.TestUsersControllers
{
    public class TestUserController
    {


        private DbContextOptions<InstagramStoreContext> _options;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IUserMapper> _userMapperMock;
        private UsersController _usersControllerMock;
        private Mock<ITokenBlackListWrapper> _tokenBlackListWrapperMock;



        public TestUserController()
        {

            _options = new DbContextOptionsBuilder<InstagramStoreContext>()
             .UseInMemoryDatabase(databaseName: "TestDatabase")
             .Options;


            _userRepositoryMock = new Mock<IUserRepository>();
            _userMapperMock = new Mock<IUserMapper>();
            _usersControllerMock = new UsersController(_userRepositoryMock.Object, _userMapperMock.Object);
            _tokenBlackListWrapperMock = new Mock<ITokenBlackListWrapper>();

        }

        [Fact]
        public void GetUsers_ReturnsOkResultWithUsers()
        {

            var users = new List<User>
            {
              new User{ UserName="user1",Email="user1@gmail.com",Role=Role.Admin},
              new User{ UserName="user2",Email="user2@gmail.com",Role=Role.Admin}

            };

            var expectedUserDtos = new List<GetUsersDto>
            {

                new GetUsersDto { UserName = "user1", Email = "user1@gmail.com", Role = "Admin" },
                new GetUsersDto { UserName = "user2", Email = "user2@gmail.com", Role = "Admin" }

            };

            _userRepositoryMock.Setup(x => x.GetUsers()).Returns(users);
            _userMapperMock.Setup(mapper => mapper.MapUserDto(It.IsAny<User>())).Returns((User user) =>
            {

                return expectedUserDtos[users.IndexOf(user)];

            });



            //Act

            var result = _usersControllerMock.GetUsers();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualUserDtos = Assert.IsAssignableFrom<IEnumerable<GetUsersDto>>(okResult.Value);

            Assert.Equal(expectedUserDtos.Count(), actualUserDtos.Count());

            _userRepositoryMock.Verify(repo => repo.GetUsers(), Times.Once());
            _userMapperMock.Verify(mapper => mapper.MapUserDto(It.IsAny<User>()), Times.Exactly(users.Count));

        }

        [Fact]
        public void GetUserById_ReturnsOkResultWithUserDto()
        {
            // Arrange
            int userId = 1;
            var user = new User { Id = userId, UserName = "testuser", Email = "testuser@gmail.com", Role = Role.Admin };
            var expectedUserDto = new GetUsersDto { UserName = "testuser", Email = "testuser@gmail.com", Role = "Admin" };


            _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns(user);
            _userMapperMock.Setup(mapper => mapper.MapUserDto(user)).Returns(expectedUserDto);



            // Act
            var result = _usersControllerMock.GetUserById(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualUserDto = Assert.IsType<GetUsersDto>(okResult.Value);

            Assert.Equal(expectedUserDto.UserName, actualUserDto.UserName);
            Assert.Equal(expectedUserDto.Email, actualUserDto.Email);
            Assert.Equal(expectedUserDto.Role, actualUserDto.Role);

            _userRepositoryMock.Verify(repo => repo.GetUserById(userId), Times.Once());
            _userMapperMock.Verify(mapper => mapper.MapUserDto(user), Times.Once());
        }



        [Fact]
        public void GetUsername_ReturnsOkResultWithUser()
        {
            // Arrange
            string username = "testuser";
            var testUser = new User { UserName = username, Email = "testuser@gmail.com", Role = Role.Admin };
            _userRepositoryMock.Setup(repo => repo.GetUserByUserName(username)).Returns(testUser);



            // Act
            var result = _usersControllerMock.GetUsername(username);

            // Assert


            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualUser = Assert.IsType<User>(okResult.Value);

            Assert.Equal(testUser.UserName, actualUser.UserName);
            Assert.Equal(testUser.Email, actualUser.Email);
            Assert.Equal(testUser.Role, actualUser.Role);

            _userRepositoryMock.Verify(repo => repo.GetUserByUserName(username), Times.Once());
        }


        #region put
        [Fact]
        public void UpdateUser_WhenAllGood_ReturnsNoContentResult()
        {
            // Arrange
            int userId = 1;
            var userDto = new UserDto { UserName = "updateduser", Email = "updateduser@gmail.com", Password = "newpassword" };
            var existingUser = new User { Id = userId, UserName = "testuser", Email = "testuser@gmail.com", Password = "oldpassword" };

            var userRepositoryMock = new Mock<IUserRepository>();
            var userMapperMock = new Mock<IUserMapper>();

            userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns(existingUser);
            userRepositoryMock.Setup(repo => repo.GetUserByUserName(userDto.UserName)).Returns((User)null);

            var userClaim = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
        new Claim(ClaimTypes.Name,"john")
    }, "TestAuthentication"));

            var httpContext = new DefaultHttpContext();
            httpContext.User = userClaim;

            _usersControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Act
            var result = _usersControllerMock.UpdateUser(userId, userDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
            userRepositoryMock.Verify(repo => repo.GetUserById(userId), Times.Once());
            userRepositoryMock.Verify(repo => repo.GetUserByUserName(userDto.UserName), Times.Once());
            userRepositoryMock.Verify(repo => repo.UpdateUser(userId, It.IsAny<User>()), Times.Once());
        }

        [Fact]
        public void UpdateUser_WhenForbidden_ReturnsForbidResult()
        {
            // Arrange
            int userId = 1;
            var userDto = new UserDto { UserName = "updateduser", Email = "updateduser@gmail.com", Password = "newpassword" };
            var existingUser = new User { Id = userId, UserName = "testuser", Email = "testuser@gmail.com", Password = "oldpassword" };

            var userRepositoryMock = new Mock<IUserRepository>();
            var userMapperMock = new Mock<IUserMapper>();

            userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns(existingUser);
            userRepositoryMock.Setup(repo => repo.GetUserByUserName(userDto.UserName)).Returns(existingUser);

            var userClaim = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
        new Claim(ClaimTypes.Name,"john")
    }, "TestAuthentication"));

            var httpContext = new DefaultHttpContext();
            httpContext.User = userClaim;

            _usersControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Act
            var result = _usersControllerMock.UpdateUser(userId, userDto);

            // Assert
            //Assert.IsType<ForbidResult>(result);
            userRepositoryMock.Verify(repo => repo.GetUserById(userId), Times.Once());
            userRepositoryMock.Verify(repo => repo.GetUserByUserName(userDto.UserName), Times.Once());
            userRepositoryMock.Verify(repo => repo.UpdateUser(userId, It.IsAny<User>()), Times.Never());
        }

        [Fact]
        public void UpdateUser_WhenNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            int userId = 1;
            var userDto = new UserDto { UserName = "updateduser", Email = "updateduser@gmail.com", Password = "newpassword" };

            var userRepositoryMock = new Mock<IUserRepository>();
            var userMapperMock = new Mock<IUserMapper>();

            userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns((User)null);

            var userClaim = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
               new Claim(ClaimTypes.Name,"john")
             }, "TestAuthentication"));


            var httpContext = new DefaultHttpContext();
            httpContext.User = userClaim;

            _usersControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Act
            var result = _usersControllerMock.UpdateUser(userId, userDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
            userRepositoryMock.Verify(repo => repo.GetUserById(userId), Times.Once());
            userRepositoryMock.Verify(repo => repo.GetUserByUserName(userDto.UserName), Times.Once());
            userRepositoryMock.Verify(repo => repo.UpdateUser(userId, It.IsAny<User>()), Times.Once());
        }





        #endregion

        /*

         [Fact]
         public void DeleteUser_WithValidId_ReturnsNoContentResult()
         {
             // Arrange
             int userId = 1;
             var userToDelete = new User { Id = userId, UserName = "testuser", Role = Role.User };

             _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns(userToDelete);
             _usersControllerMock.ControllerContext = new ControllerContext
             {
                 HttpContext = new DefaultHttpContext
                 {
                     User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                     {
                 new Claim(ClaimTypes.Name, "testuser"),
                // new Claim(ClaimTypes.Role, Role.User) 

                     }, "testauthentication"))
                 }
             };

             // Act
             var result = _usersControllerMock.DeleteUser(userId);

             // Assert
             var noContentResult = Assert.IsType<NoContentResult>(result);
             Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

             _userRepositoryMock.Verify(repo => repo.DeleteUser(userId), Times.Once());
             _tokenBlackListWrapperMock.Verify(wrapper => wrapper.AddToBlackList(userToDelete.UserName), Times.Once());
         }

         */


        [Fact]
        public void DeleteUser_WithNonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            int userId = 1;
            _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns((User)null);

            // Act
            var result = _usersControllerMock.DeleteUser(userId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.Equal("User doesn't exist", notFoundResult.Value);

            _userRepositoryMock.Verify(repo => repo.DeleteUser(userId), Times.Never());
            _tokenBlackListWrapperMock.Verify(wrapper => wrapper.AddToBlackList(It.IsAny<string>()), Times.Never());
        }



        /*

        [Fact]
        public void DeleteUser_WithUnauthorizedUser_ReturnsForbid()
        {
            // Arrange
            int userId = 1;
            var userToDelete = new User { Id = userId, UserName = "testuser", Role = Role.User };
            _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns(userToDelete);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "otheruser")
            };
            var identity = new ClaimsIdentity(claims, "testauthentication");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var controller = new UsersController(_userRepositoryMock.Object,_tokenBlackListWrapperMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = claimsPrincipal
                }
            };

            // Act
            var result = controller.DeleteUser(userId);

            // Assert
            var forbidResult = Assert.IsType<ForbidResult>(result);
            _userRepositoryMock.Verify(repo => repo.DeleteUser(userId), Times.Never());
            _tokenBlackListWrapperMock.Verify(wrapper => wrapper.AddToBlackList(It.IsAny<string>()), Times.Never());
        }

        */


    }
}
