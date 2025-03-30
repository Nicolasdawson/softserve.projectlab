using AutoMapper;
using API.Data.Entities;
using API.Models.Logistics;
using API.Models.Logistics.Interfaces;
using API.Models.IntAdmin;

public class LogisticsMapping : Profile
{
    public LogisticsMapping()
    {
        // Map from WarehouseEntity to IWarehouse
        CreateMap<WarehouseEntity, IWarehouse>()
            .ForMember(dest => dest.WarehouseId, opt => opt.MapFrom(src => src.WarehouseId))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));
        // Add other property mappings here

        // CreateMap<WarehouseEntity, Warehouse>(); // Map to concrete class
        CreateMap<ItemEntity, Item>(); // Add other necessary mappings
    }
}
