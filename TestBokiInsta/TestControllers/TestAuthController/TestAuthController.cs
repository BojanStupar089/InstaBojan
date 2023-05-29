using InstaBojan.Controllers.AuthControllers;
using InstaBojan.Core.Models;
using InstaBojan.Core.Security;
using InstaBojan.Dtos.UsersDto;
using InstaBojan.Infrastructure.Data;
using InstaBojan.Infrastructure.Repository.UsersRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.IdentityModel.Tokens.Jwt;

namespace TestBokiInsta.TestControllers.TestAuthController
{
    public class TestAuthController
    {

        private DbContextOptions<InstagramStoreContext> _options;
        private Mock<IUserRepository> _usersRepositoryMock;
        private AuthController _authControllerMock;


        public TestAuthController()
        {

            _options = new DbContextOptionsBuilder<InstagramStoreContext>()
           .UseInMemoryDatabase(databaseName: "TestDatabase")
           .Options;

            _usersRepositoryMock = new Mock<IUserRepository>();
            _authControllerMock = new AuthController(_usersRepositoryMock.Object);


        }

        [Fact]
        public void Register_WhenUserDoesNotExist_ReturnsOk()
        {
            // Arrange
            var userDto = new UserDto
            {
                UserName = "john123",
                Email = "john@example.com",
                Password = "password"
            };

            _usersRepositoryMock.Setup(repo => repo.GetUserByUserName(userDto.UserName)).Returns((User)null);
            _usersRepositoryMock.Setup(repo => repo.AddUser(It.IsAny<User>()));

            // Act
            var result = _authControllerMock.Register(userDto);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Register_WhenUserExists_ReturnsBadRequest()
        {
            // Arrange
            var userDto = new UserDto
            {
                UserName = "john123",
                Email = "john@example.com",
                Password = "password"
            };

            var existingUser = new User
            {
                UserName = "john123",
                Email = "john@example.com"
            };

            _usersRepositoryMock.Setup(repo => repo.GetUserByUserName(userDto.UserName)).Returns(existingUser);

            // Act
            var result = _authControllerMock.Register(userDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Login_ReturnsToken_WhenCredentialsAreCorrect()
        {
            // Arrange
            var user = new User
            {
                UserName = "validUsername",
                Password = BCrypt.Net.BCrypt.HashPassword("validPassword"),
                Role = Role.User
            };

            _usersRepositoryMock.Setup(repo => repo.GetUserByUserName("validUsername")).Returns(user);
            var loginModel = new LoginModel
            {
                UserName = "validUsername",
                Password = "validPassword"
            };



            // Act
            var result = _authControllerMock.Login(loginModel);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            var token = okResult.Value as string;
            Assert.NotNull(token);


            var jwtHandler = new JwtSecurityTokenHandler();
            var isValidToken = jwtHandler.CanReadToken(token);
            Assert.True(isValidToken);
        }

        [Fact]
        public void Login_ReturnsBadRequest_WhenUsernameIsInvalid()
        {
            // Arrange
            _usersRepositoryMock.Setup(repo => repo.GetUserByUserName("invalidUsername")).Returns((User)null);
            var loginModel = new LoginModel
            {
                UserName = "invalidUsername",
                Password = "validPassword"
            };

            // Act
            var result = _authControllerMock.Login(loginModel);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal("Invalid username", badRequestResult.Value);
        }

        [Fact]
        public void Login_ReturnsBadRequest_WhenPasswordIsInvalid()
        {
            // Arrange
            var user = new User
            {
                UserName = "validUsername",
                Password = BCrypt.Net.BCrypt.HashPassword("validPassword")
            };

            _usersRepositoryMock.Setup(repo => repo.GetUserByUserName("validUsername")).Returns(user);
            var loginModel = new LoginModel
            {
                UserName = "validUsername",
                Password = "invalidPassword"
            };

            // Act
            var result = _authControllerMock.Login(loginModel);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal("Wrong password", badRequestResult.Value);
        }


        [Fact]
        public void GenerateToken_ReturnsValidJwtToken()
        {
            // Arrange
            var user = new User
            {
                UserName = "testUser",
                Role = Role.Admin
            };



            // Act
            //var token = _authControllerMock.GenerateToken(user);

            // Assert
           // var jwtHandler = new JwtSecurityTokenHandler();
          //  var isValidToken = jwtHandler.CanReadToken(token);
          //  Assert.True(isValidToken);
        }


    }
}
