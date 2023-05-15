using AutoMapper;
using InstaBojan.Core.Models;
using InstaBojan.Core.Security;
using InstaBojan.Dtos;

namespace InstaBojan.Helpers
{
    public class MappingProfile:Profile
    {

        public MappingProfile() { 
        
            CreateMap<User,UserDto>().ReverseMap();
        }  
    }
}
