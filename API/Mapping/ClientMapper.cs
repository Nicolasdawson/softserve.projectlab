using API.Data.Models.DTOs;
using API.Data.Models;
using AutoMapper;
using API.Utils;

namespace API.Mapping
{
    public class ClientMapper : Profile
    {
        public ClientMapper()
        {
            CreateMap<Client, ClientDtoOut>().ReverseMap();
            CreateMap<Client, ClientDtoIn>().ReverseMap()
                        .ForMember(dest => dest.Password, opt => opt.MapFrom(src => PasswordHasher.HashPassword(src.Password)));

        }
    }
}
