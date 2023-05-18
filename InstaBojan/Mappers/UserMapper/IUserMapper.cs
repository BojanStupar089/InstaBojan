using InstaBojan.Core.Models;
using InstaBojan.Dtos;

namespace InstaBojan.Mappers.UserMapper
{
    public interface IUserMapper
    {
        public UserDto MapUserDto(User user);
        public User MapUser(UserDto userDto);
        public List<UserDto> MapListUserDto(List<User> users);
    }
}
