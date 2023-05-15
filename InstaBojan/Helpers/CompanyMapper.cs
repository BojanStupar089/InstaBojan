using AutoMapper;
using InstaBojan.Core.Models;
using InstaBojan.Core.Security;
using InstaBojan.Dtos;

namespace InstaBojan.Helpers
{
    public class CompanyMapper:ICompanyMapper
    {

        public CompanyMapper() { 
        
           
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
            Mapper mapper=new Mapper(configuration);

            return mapper.Map<User, UserDto>(user);
        }
    }
}
