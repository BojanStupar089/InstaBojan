using AutoMapper;
using InstaBojan.Core.Models;
using InstaBojan.Dtos;

namespace InstaBojan.Mappers.UserMapper
{
    public class UserMapper:IUserMapper
    {
        public List<UserDto> MapListUserDto(List<User> users)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDto>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<List<User>,List<UserDto>>(users);
        }

        public User MapUser(UserDto userDto)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<UserDto, User>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<UserDto, User>(userDto);
        }

        public UserDto MapUserDto(User user)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDto>());
            Mapper mapper = new Mapper(configuration);
            return mapper.Map<User, UserDto>(user);
        }



       
    }
}
