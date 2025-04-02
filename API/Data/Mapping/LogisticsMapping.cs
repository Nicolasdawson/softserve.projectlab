using AutoMapper;
using API.Data.Entities;
using API.Models.Logistics;
using API.Models.Logistics.Interfaces;
using API.Models.IntAdmin;

public class LogisticsMapping : Profile
{
    public LogisticsMapping()
    {
        // Map from WarehouseEntity to Warehouse (concrete class)
        CreateMap<WarehouseEntity, Warehouse>()
            .ForMember(dest => dest.WarehouseId, opt => opt.MapFrom(src => src.WarehouseId))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));
        // Add other property mappings here

        // Map from ItemEntity to Item
        CreateMap<ItemEntity, Item>()
            .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.Sku))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.OriginalStock, opt => opt.MapFrom(src => src.OriginalStock))
            .ForMember(dest => dest.CurrentStock, opt => opt.MapFrom(src => src.CurrentStock))
            .ForMember(dest => dest.ItemCurrency, opt => opt.MapFrom(src => src.Currency))
            .ForMember(dest => dest.UnitCost, opt => opt.MapFrom(src => src.UnitCost))
            .ForMember(dest => dest.MarginGain, opt => opt.MapFrom(src => src.MarginGain))
            .ForMember(dest => dest.ItemDiscount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.AdditionalTax, opt => opt.MapFrom(src => src.AdditionalTax))
            .ForMember(dest => dest.ItemPrice, opt => opt.MapFrom(src => src.ItemPrice))
            .ForMember(dest => dest.ItemStatus, opt => opt.MapFrom(src => src.ItemStatus))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.ItemImage, opt => opt.MapFrom(src => src.Image));

        // Map from Item to ItemEntity
        CreateMap<Item, ItemEntity>()
            .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.Sku))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.OriginalStock, opt => opt.MapFrom(src => src.OriginalStock))
            .ForMember(dest => dest.CurrentStock, opt => opt.MapFrom(src => src.CurrentStock))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.ItemCurrency))
            .ForMember(dest => dest.UnitCost, opt => opt.MapFrom(src => src.UnitCost))
            .ForMember(dest => dest.MarginGain, opt => opt.MapFrom(src => src.MarginGain))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.ItemDiscount))
            .ForMember(dest => dest.AdditionalTax, opt => opt.MapFrom(src => src.AdditionalTax))
            .ForMember(dest => dest.ItemPrice, opt => opt.MapFrom(src => src.ItemPrice))
            .ForMember(dest => dest.ItemStatus, opt => opt.MapFrom(src => src.ItemStatus))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ItemImage));
    }
}
