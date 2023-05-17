using InstaBojan.Core.Models;
using InstaBojan.Dtos;

namespace InstaBojan.Helpers
{
    public interface ICompanyMapper
    {
        public UserDto MapUserDto(User user);
        public User MapUser(UserDto userDto);
        public PostDto MapPostDto(Post post);
        public Post MapPost(PostDto postDto);
        public ProfileDto MapProfileDto(Profile profile);
        public Profile MapProfile(ProfileDto profileDto);
    }
}
