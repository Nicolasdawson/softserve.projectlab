using API.Data.Models;
using API.Data.Models.DTOs;
using AutoMapper;

namespace API.Mapping
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductDtoIn>().ReverseMap();
            CreateMap<Product, ProductDtoOut>()
              .ForMember(dest => dest.ProductCategoryName, opt => opt.MapFrom(src => src.Category.Category)) // Ajustado a ProductCategory.Name
              .ReverseMap();
        }
    }
}
