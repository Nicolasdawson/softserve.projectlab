using API.Data.Models;
using API.Data.Models.DTOs;
using AutoMapper;

namespace API.Mapping
{
    public class ProductCategoryMapper : Profile
    {
        public ProductCategoryMapper()
        {
            CreateMap<ProductCategory, ProductCategoryDtoIn>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryDtoOut>().ReverseMap();
        }
    }
}
