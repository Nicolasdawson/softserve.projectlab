using API.Data.Models;
using API.Data.Models.DTOs.Category;
using API.Data.Models.DTOs.Product;
using API.Data.Models.DTOs.ShoppingCart;
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
            CreateMap<Product, ProductPostDto>().ReverseMap();
            CreateMap<Product, ProductGetDto>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name)).ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            CreateMap<ShoppingCartItem, ShoppingCartPostDto>().ReverseMap();


            
        }
    }
}
