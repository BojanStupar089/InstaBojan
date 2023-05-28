using InstaBojan.Controllers.ProfilesController;
using InstaBojan.Core.Models;
using InstaBojan.Dtos.ProfilesDto;
using InstaBojan.Infrastructure.Data;
using InstaBojan.Infrastructure.Repository.ProfilesRepository;
using InstaBojan.Infrastructure.Repository.UsersRepository;
using InstaBojan.Mappers.ProfileMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;

namespace TestBokiInsta.TestControllers.TestProfilesController
{
    public class TestProfilesController
    {



        private DbContextOptions<InstagramStoreContext> _options;
        private Mock<IProfilesRepository> _profilesRepositoryMock;
        private Mock<IProfileMapper> _profilesMapperMock;
        private ProfilesController _profilesControllerMock;
        private Mock<IUserRepository> _usersRepositoryMock;


        public TestProfilesController()
        {


            _options = new DbContextOptionsBuilder<InstagramStoreContext>()
              .UseInMemoryDatabase(databaseName: "TestDatabase")
              .Options;


            _usersRepositoryMock = new Mock<IUserRepository>();
            _profilesMapperMock = new Mock<IProfileMapper>();
            _profilesRepositoryMock = new Mock<IProfilesRepository>();
            _profilesControllerMock = new ProfilesController(_profilesRepositoryMock.Object, _profilesMapperMock.Object, _usersRepositoryMock.Object);


        }


        [Fact]
        public void GetProfiles_ReturnsOkResultWithProfiles()
        {
            // Arrange
            var profiles = new List<Profile>
    {
        new Profile { Id = 1, ProfileName = "profile1", ProfilePicture = "picture1",User=new User{ UserName="bla",Email="bla@gmail.com",Password="bla"} ,FirstName = "John", LastName = "Doe", Birthday = DateTime.Now, Gender = "Male", Followers = new List<Profile>(), Following = new List<Profile>() },
        new Profile { Id = 2, ProfileName = "profile2", ProfilePicture = "picture2",User=new User{UserName="bla",Email="bla@gmail.com",Password="bla" }, FirstName = "Jane", LastName = "Smith", Birthday = DateTime.Now, Gender = "Female", Followers = new List<Profile>(), Following = new List<Profile>() }
    };

            _profilesRepositoryMock.Setup(repo => repo.GetProfiles()).Returns(profiles);
            _profilesMapperMock.Setup(mapper => mapper.MapGetProfilesDto(It.IsAny<Profile>())).Returns<Profile>(p =>
                new GetProfilesDto
                {
                    ProfileName = p.ProfileName,
                    ProfilePicture = p.ProfilePicture,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    BirthDay = p.Birthday,
                    Gender = p.Gender,
                    NumberFollowers = p.Followers?.Count ?? 0,
                    NumberFollowing = p.Following?.Count ?? 0
                }


                );

            // Act
            var result = _profilesControllerMock.GetProfiles();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var profilesDto = okResult.Value as List<GetProfilesDto>;
            //var profilesDto = 2;
            Assert.NotNull(profilesDto); // Ensure profilesDto is not null
            Assert.Equal(profiles.Count, profilesDto.Count);
            foreach (var profileDto in profilesDto)
            {
                Assert.Contains(profiles, p =>
                    p.ProfileName == profileDto.ProfileName &&
                    p.ProfilePicture == profileDto.ProfilePicture &&
                    p.FirstName == profileDto.FirstName &&
                    p.LastName == profileDto.LastName &&
                    p.Birthday == profileDto.BirthDay &&
                    p.Gender == profileDto.Gender &&
                    p.Followers?.Count == profileDto.NumberFollowers &&
                    p.Following?.Count == profileDto.NumberFollowing);
            }



        }



        [Fact]
        public void GetProfileByUserName_WithExistingUsername_ReturnsOkWithProfileDto()
        {
            // Arrange
            var username = "user1"; // Set the username value here
            var profile = new Profile
            {
                ProfileName = "profile1",
                ProfilePicture = "picture1",
                FirstName = "John",
                LastName = "Doe",
                Birthday = DateTime.Now,
                Gender = "Male",
                Followers = new List<Profile>(),
                Following = new List<Profile>()
            };

            _profilesRepositoryMock.Setup(repo => repo.GetProfileByUserName(username)).Returns(profile);
            _profilesMapperMock.Setup(mapper => mapper.MapGetProfilesDto(profile)).Returns(new GetProfilesDto
            {
                ProfileName = profile.ProfileName,
                ProfilePicture = profile.ProfilePicture,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                BirthDay = profile.Birthday,
                Gender = profile.Gender,
                NumberFollowers = profile.Followers.Count,
                NumberFollowing = profile.Following.Count
            });

            // Act
            var result = _profilesControllerMock.GetProfileByUserName(username);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var profileDto = Assert.IsType<GetProfilesDto>(okResult.Value);
            Assert.Equal(profile.ProfileName, profileDto.ProfileName);
            Assert.Equal(profile.ProfilePicture, profileDto.ProfilePicture);
            Assert.Equal(profile.FirstName, profileDto.FirstName);
            Assert.Equal(profile.LastName, profileDto.LastName);
            Assert.Equal(profile.Birthday, profileDto.BirthDay);
            Assert.Equal(profile.Gender, profileDto.Gender);
            Assert.Equal(profile.Followers.Count, profileDto.NumberFollowers);
            Assert.Equal(profile.Following.Count, profileDto.NumberFollowing);
        }

        [Fact]
        public void GetProfileByUserName_WithNonExistingUsername_ReturnsNotFound()
        {
            // Arrange
            var username = "nonexistentuser"; // Set a non-existing username here

            _profilesRepositoryMock.Setup(repo => repo.GetProfileByUserName(username)).Returns((Profile)null);

            // Act
            var result = _profilesControllerMock.GetProfileByUserName(username);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetProfileById_WithExistingId_ReturnsOkWithProfileDto()
        {
            // Arrange
            var id = 1; // Set the existing profile ID here
            var profile = new Profile
            {
                Id = id,
                ProfileName = "profile1",
                ProfilePicture = "picture1",
                FirstName = "John",
                LastName = "Doe",
                Birthday = DateTime.Now,
                Gender = "Male",
                Followers = new List<Profile>(),
                Following = new List<Profile>()
            };

            _profilesRepositoryMock.Setup(repo => repo.GetProfileById(id)).Returns(profile);
            _profilesMapperMock.Setup(mapper => mapper.MapGetProfilesDto(profile)).Returns(new GetProfilesDto
            {
                ProfileName = profile.ProfileName,
                ProfilePicture = profile.ProfilePicture,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                BirthDay = profile.Birthday,
                Gender = profile.Gender,
                NumberFollowers = profile.Followers.Count,
                NumberFollowing = profile.Following.Count
            });

            // Act
            var result = _profilesControllerMock.GetProfileById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var profileDto = Assert.IsType<GetProfilesDto>(okResult.Value);
            Assert.Equal(profile.ProfileName, profileDto.ProfileName);
            Assert.Equal(profile.ProfilePicture, profileDto.ProfilePicture);
            Assert.Equal(profile.FirstName, profileDto.FirstName);
            Assert.Equal(profile.LastName, profileDto.LastName);
            Assert.Equal(profile.Birthday, profileDto.BirthDay);
            Assert.Equal(profile.Gender, profileDto.Gender);
            Assert.Equal(profile.Followers.Count, profileDto.NumberFollowers);
            Assert.Equal(profile.Following.Count, profileDto.NumberFollowing);
        }

        [Fact]
        public void GetProfileById_WithNonExistingId_ReturnsNotFound()
        {
            // Arrange
            var id = 999; // Set a non-existing profile ID here

            _profilesRepositoryMock.Setup(repo => repo.GetProfileById(id)).Returns((Profile)null);

            // Act
            var result = _profilesControllerMock.GetProfileById(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetProfileByUserId_WithExistingUserId_ReturnsOkWithProfileDto()
        {
            // Arrange
            var userId = 1; // Set the existing user ID here
            var profile = new Profile
            {
                Id = 1,
                ProfileName = "profile1",
                ProfilePicture = "picture1",
                UserId = userId,
                User = new User(),
                FirstName = "John",
                LastName = "Doe",
                Birthday = DateTime.Now,
                Gender = "Male",
                Followers = new List<Profile>(),
                Following = new List<Profile>()
            };

            _profilesRepositoryMock.Setup(repo => repo.GetProfileByUserId(userId)).Returns(profile);
            _profilesMapperMock.Setup(mapper => mapper.MapGetProfilesDto(profile)).Returns(new GetProfilesDto
            {
                ProfileName = profile.ProfileName,
                ProfilePicture = profile.ProfilePicture,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                BirthDay = profile.Birthday,
                Gender = profile.Gender,
                NumberFollowers = profile.Followers.Count,
                NumberFollowing = profile.Following.Count
            });

            // Act
            var result = _profilesControllerMock.GetProfileByUserId(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var profileDto = Assert.IsType<GetProfilesDto>(okResult.Value);
            Assert.Equal(profile.ProfileName, profileDto.ProfileName);
            Assert.Equal(profile.ProfilePicture, profileDto.ProfilePicture);
            Assert.Equal(profile.FirstName, profileDto.FirstName);
            Assert.Equal(profile.LastName, profileDto.LastName);
            Assert.Equal(profile.Birthday, profileDto.BirthDay);
            Assert.Equal(profile.Gender, profileDto.Gender);
            Assert.Equal(profile.Followers.Count, profileDto.NumberFollowers);
            Assert.Equal(profile.Following.Count, profileDto.NumberFollowing);
        }

        [Fact]
        public void GetProfileByUserId_WithNonExistingUserId_ReturnsNotFound()
        {
            // Arrange
            var userId = 999; // Set a non-existing user ID here

            _profilesRepositoryMock.Setup(repo => repo.GetProfileByUserId(userId)).Returns((Profile)null);

            // Act
            var result = _profilesControllerMock.GetProfileByUserId(userId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public void GetProfileByProfileName_WithExistingProfileName_ReturnsOkWithProfileDto()
        {
            // Arrange
            var profileName = "profile1"; // Set the existing profile name here
            var profile = new Profile
            {
                Id = 1,
                ProfileName = profileName,
                ProfilePicture = "picture1",
                UserId = 1,
                User = new User(),
                FirstName = "John",
                LastName = "Doe",
                Birthday = DateTime.Now,
                Gender = "Male",
                Followers = new List<Profile>(),
                Following = new List<Profile>()
            };

            _profilesRepositoryMock.Setup(repo => repo.GetProfileByProfileName(profileName)).Returns(profile);
            _profilesMapperMock.Setup(mapper => mapper.MapGetProfilesDto(profile)).Returns(new GetProfilesDto
            {
                ProfileName = profile.ProfileName,
                ProfilePicture = profile.ProfilePicture,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                BirthDay = profile.Birthday,
                Gender = profile.Gender,
                NumberFollowers = profile.Followers.Count,
                NumberFollowing = profile.Following.Count
            });

            // Act
            var result = _profilesControllerMock.GetProfileByProfileName(profileName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var profileDto = Assert.IsType<GetProfilesDto>(okResult.Value);
            Assert.Equal(profile.ProfileName, profileDto.ProfileName);
            Assert.Equal(profile.ProfilePicture, profileDto.ProfilePicture);
            Assert.Equal(profile.FirstName, profileDto.FirstName);
            Assert.Equal(profile.LastName, profileDto.LastName);
            Assert.Equal(profile.Birthday, profileDto.BirthDay);
            Assert.Equal(profile.Gender, profileDto.Gender);
            Assert.Equal(profile.Followers.Count, profileDto.NumberFollowers);
            Assert.Equal(profile.Following.Count, profileDto.NumberFollowing);
        }

        [Fact]
        public void GetProfileByProfileName_WithNonExistingProfileName_ReturnsNotFound()
        {
            // Arrange
            var profileName = "nonexisting"; // Set a non-existing profile name here

            _profilesRepositoryMock.Setup(repo => repo.GetProfileByProfileName(profileName)).Returns((Profile)null);

            // Act
            var result = _profilesControllerMock.GetProfileByProfileName(profileName);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }





        #region post
        [Fact]
        public void AddProfiles_WithValidProfileDto_ReturnsCreatedResult()
        {
            // Arrange
            var profileDto = new ProfileDto
            {
                ProfileName = "profile1",
                ProfilePicture = "picture1",
                FirstName = "John",
                LastName = "Doe",
                BirthDay = DateTime.Now,
                Gender = "Male"
            };

            var username = "user1";
            var user = new User { Id = 1, UserName = username, Email = "user@gmail.com", Password = "user" };
            _usersRepositoryMock.Setup(repo => repo.GetUserByUserName(username)).Returns(user);



            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, username) // Add any additional claims as needed
    };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);

            _profilesControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = principal
                }
            };

            _profilesRepositoryMock.Setup(repo => repo.GetProfileByProfileName(profileDto.ProfileName)).Returns((Profile)null);

            var mappedProfile = new Profile { Id = 3, ProfileName = profileDto.ProfileName };
            _profilesMapperMock.Setup(mapper => mapper.MapProfile(profileDto)).Returns(mappedProfile);

            _profilesRepositoryMock.Setup(repo => repo.AddProfile(mappedProfile));



            // Act
            var result = _profilesControllerMock.AddProfiles(profileDto);

            // Assert
            Assert.IsType<CreatedResult>(result);
            var createdResult = result as CreatedResult;
            Assert.Equal("api/profiles/" + mappedProfile.Id, createdResult?.Location);
            Assert.Equal(profileDto, createdResult?.Value);
            _profilesRepositoryMock.Verify(repo => repo.AddProfile(mappedProfile), Times.Once);
        }

        [Fact]
        public void AddProfiles_WithExistingProfileName_ReturnsBadRequest()
        {
            // Arrange
            var profileDto = new ProfileDto
            {
                ProfileName = "profile1",
                ProfilePicture = "picture1",
                FirstName = "John",
                LastName = "Doe",
                BirthDay = DateTime.Now,
                Gender = "Male"
            };

            var existingProfile = new Profile { ProfileName = profileDto.ProfileName };
            _profilesRepositoryMock.Setup(repo => repo.GetProfileByProfileName(profileDto.ProfileName)).Returns(existingProfile);

            var username = "user1";
            _usersRepositoryMock.Setup(repo => repo.GetUserByUserName(username)).Returns(new User());

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, username)
    };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = principal
            };

            _profilesControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Act
            var result = _profilesControllerMock.AddProfiles(profileDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.Equal("Profile Name already exists", badRequestResult?.Value);
            _profilesRepositoryMock.Verify(repo => repo.AddProfile(It.IsAny<Profile>()), Times.Never);
        }



        [Fact]
        public void AddProfiles_WithNonExistingUser_ReturnsNotFound()
        {
            // Arrange
            var profileDto = new ProfileDto
            {
                ProfileName = "profile1",
                ProfilePicture = "picture1",
                FirstName = "John",
                LastName = "Doe",
                BirthDay = DateTime.Now,
                Gender = "Male"
            };

            var username = "user1";
            _usersRepositoryMock.Setup(repo => repo.GetUserByUserName(username)).Returns((User)null);

            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, username)
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = principal
            };

            _profilesControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Act
            var result = _profilesControllerMock.AddProfiles(profileDto);

            // Assert
            Assert.IsType<NotFoundResult>(result);





        }



        [Fact]
        public void AddFollowing_WithValidFollowingId_ReturnsOkResult()
        {
            // Arrange
            var followingId = 2;
            var username = "user1";

            var userProfile = new Profile { Id = 1 };
            _profilesRepositoryMock.Setup(repo => repo.GetProfileByUserName(username)).Returns(userProfile);

            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, username)
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = principal
            };

            _profilesControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };



            // Act
            var result = _profilesControllerMock.AddFollowing(followingId);

            // Assert
            Assert.IsType<OkResult>(result);
            _profilesRepositoryMock.Verify(repo => repo.AddFollowing(userProfile.Id, followingId), Times.Once);
        }

        #endregion

        #region put 


        [Fact]
        public void UpdateProfiles_WithValidIdAndProfileDto_ReturnsNoContent()
        {
            // Arrange
            var id = 1;
            var updateProfileDto = new ProfileDto
            {
                ProfileName = "updatedProfile",
                ProfilePicture = "updatedPicture",
                FirstName = "John",
                LastName = "Doe",
                BirthDay = DateTime.Now,
                Gender = "Male"
            };

            var username = "user1";
            var profile = new Profile { Id = id };
            _profilesRepositoryMock.Setup(repo => repo.GetProfileById(id)).Returns(profile);
            _profilesRepositoryMock.Setup(repo => repo.GetProfileByUserName(username)).Returns(profile);
            _profilesRepositoryMock.Setup(repo => repo.GetProfileByProfileName(updateProfileDto.ProfileName)).Returns((Profile)null);


            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, username)
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = principal
            };

            _profilesControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Act
            var result = _profilesControllerMock.UpdateProfiles(id, updateProfileDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _profilesRepositoryMock.Verify(repo => repo.UpdateProfile(id, It.IsAny<Profile>()), Times.Once);
        }

        [Fact]
        public void UpdateProfiles_WithNonAdminUserAndMismatchedProfileId_ReturnsForbid()
        {
            // Arrange
            var id = 1;
            var updateProfileDto = new ProfileDto { /* Profile DTO details */ };

            var username = "user1";
            var profile = new Profile { Id = 2 }; // Profile ID doesn't match
            _profilesRepositoryMock.Setup(repo => repo.GetProfileById(id)).Returns(profile);
            _profilesRepositoryMock.Setup(repo => repo.GetProfileByUserName(username)).Returns(new Profile());


            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, username)
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = principal
            };

            _profilesControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Act
            var result = _profilesControllerMock.UpdateProfiles(id, updateProfileDto);

            // Assert
            Assert.IsType<ForbidResult>(result);
            _profilesRepositoryMock.Verify(repo => repo.UpdateProfile(id, It.IsAny<Profile>()), Times.Never);
        }


        [Fact]
        public void UpdateProfiles_WithExistingProfileName_ReturnsBadRequest()
        {
            // Arrange
            var id = 1;
            var updateProfileDto = new ProfileDto
            {
                ProfileName = "updatedProfile",
                ProfilePicture = "updatedPicture",
                FirstName = "John",
                LastName = "Doe",
                BirthDay = DateTime.Now,
                Gender = "Male"
            };

            var username = "user1";
            var profile = new Profile { Id = id };

            _profilesRepositoryMock.Setup(repo => repo.GetProfileById(id)).Returns(profile);
            _profilesRepositoryMock.Setup(repo => repo.GetProfileByUserName(username)).Returns(profile);
            _profilesRepositoryMock.Setup(repo => repo.GetProfileByProfileName(updateProfileDto.ProfileName)).Returns(new Profile());



            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, username)
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = principal
            };

            _profilesControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Act
            var result = _profilesControllerMock.UpdateProfiles(id, updateProfileDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.Equal("Profile Name already exists;", badRequestResult?.Value);
            _profilesRepositoryMock.Verify(repo => repo.UpdateProfile(id, It.IsAny<Profile>()), Times.Never);
        }


        [Fact]
        public void UpdateProfiles_WithNonExistingProfileId_ReturnsNotFound()
        {
            // Arrange
            var id = 1;
            var updateProfileDto = new ProfileDto
            {
                ProfileName = "updatedProfile",
                ProfilePicture = "updatedPicture",
                FirstName = "John",
                LastName = "Doe",
                BirthDay = DateTime.Now,
                Gender = "Male"
            };

            _profilesRepositoryMock.Setup(repo => repo.GetProfileById(id)).Returns((Profile)null);

            var username = "user1"; // Set the username value here

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, username)
    };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = principal
            };

            _profilesControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Act
            var result = _profilesControllerMock.UpdateProfiles(id, updateProfileDto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            _profilesRepositoryMock.Verify(repo => repo.UpdateProfile(id, It.IsAny<Profile>()), Times.Never);
        }






        #endregion


        #region delete


        [Fact]
        public void DeleteProfiles_WithExistingProfileId_ReturnsNoContent()
        {
            // Arrange
            var id = 1;
            var username = "user1"; // Set the username value here

            _profilesRepositoryMock.Setup(repo => repo.GetProfileById(id)).Returns(new Profile());
            _profilesRepositoryMock.Setup(repo => repo.GetProfileByUserName(username)).Returns(new Profile { Id = id });

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, username)
    };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = principal
            };

            _profilesControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Act
            var result = _profilesControllerMock.DeleteProfiles(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _profilesRepositoryMock.Verify(repo => repo.DeleteProfile(id), Times.Once);
        }

        [Fact]
        public void DeleteProfiles_WithNonExistingProfileId_ReturnsNotFound()
        {
            // Arrange
            var id = 1;
            var username = "user1"; // Set the username value here

            _profilesRepositoryMock.Setup(repo => repo.GetProfileById(id)).Returns((Profile)null);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, username)
    };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = principal
            };

            _profilesControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Act
            var result = _profilesControllerMock.DeleteProfiles(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            _profilesRepositoryMock.Verify(repo => repo.DeleteProfile(id), Times.Never);
        }

        [Fact]
        public void DeleteProfiles_WithForbiddenAccess_ReturnsForbid()
        {
            // Arrange
            var id = 1;
            var username = "user1"; // Set the username value here

            _profilesRepositoryMock.Setup(repo => repo.GetProfileById(id)).Returns(new Profile());
            _profilesRepositoryMock.Setup(repo => repo.GetProfileByUserName(username)).Returns(new Profile { Id = 2 });

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, username)
    };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = principal
            };

            _profilesControllerMock.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Act
            var result = _profilesControllerMock.DeleteProfiles(id);

            // Assert
            Assert.IsType<ForbidResult>(result);
            _profilesRepositoryMock.Verify(repo => repo.DeleteProfile(id), Times.Never);
        }




        #endregion



    }
}
