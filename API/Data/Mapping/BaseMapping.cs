using AutoMapper;
using API.Data.Entities;
using softserve.projectlabs.Shared.DTOs;

namespace API.Data.Mapping
{
    public class BaseMapping : Profile
    {
        public BaseMapping()
        {
            // Mapeo base para campos de auditoría
            CreateMap<BaseEntity, BaseDto>()
                .ReverseMap()
                // Ignoramos los campos de auditoría en el mapeo inverso
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
        }
    }
}
