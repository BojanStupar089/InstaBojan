using AutoMapper;
using InstaBojan.Core.Models;
using InstaBojan.Dtos.UsersDto;

namespace InstaBojan.Mappers.UserMapper
{
    public class UserMapper:IUserMapper
    {
       

        public User MapUser(UserDto userDto)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<UserDto, User>());
            Mapper mapper = new Mapper(configuration);

            return mapper.Map<UserDto, User>(userDto);
        }

        public GetUsersDto MapUserDto(User user)
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<User, GetUsersDto>());
            Mapper mapper = new Mapper(configuration);
            return mapper.Map< GetUsersDto>(user);
        }



       
    }
}
