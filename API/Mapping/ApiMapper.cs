using API.Data.Models;
using API.Data.Models.DTOs.Category;
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
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryGetDto>().ReverseMap();
        }
    }
}
