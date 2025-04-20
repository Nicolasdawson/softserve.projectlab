using API.Data.Models;
using API.Data.Models.DTOs.User;
using AutoMapper;

namespace API.Mapping
{
    public class ApiMapper : Profile
    {
        public ApiMapper()
        {
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserRegisterDto>().ReverseMap();
        }
    }
}
