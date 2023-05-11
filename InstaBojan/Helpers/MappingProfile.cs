using AutoMapper;
using InstaBojan.Core.Security;
using InstaBojan.Dtos.LoginDto;
using InstaBojan.Dtos.RegisterDto;

namespace InstaBojan.Helpers
{
    public class MappingProfile:Profile
    {

        public MappingProfile() {

            CreateMap<Login, LoginDto>().ReverseMap();

            CreateMap<Register, RegisterDto>().ReverseMap();
            
            
        }
    }
}
