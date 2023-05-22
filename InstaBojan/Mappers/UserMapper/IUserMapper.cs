using InstaBojan.Core.Models;
using InstaBojan.Dtos.UsersDto;

namespace InstaBojan.Mappers.UserMapper
{
    public interface IUserMapper
    {
        public GetUsersDto MapUserDto(User user);
        public User MapUser(UserDto userDto);
       
    }
}
