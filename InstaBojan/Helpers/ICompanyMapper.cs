using InstaBojan.Core.Models;
using InstaBojan.Dtos;

namespace InstaBojan.Helpers
{
    public interface ICompanyMapper
    {

        public UserDto MapUserDto(User user);

        public User MapUser(UserDto userDto);
    }
}
