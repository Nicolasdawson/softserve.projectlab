using API.Data.Entities;
using API.Models.DTOs;
using API.Models.IntAdmin;
using API.Models.Logistics.Interfaces;
using API.Models.Logistics;
using AutoMapper;
using softserve.projectlabs.Shared.DTOs;
using Logistics.Models;

public class LogisticsMapping : Profile
{
    public LogisticsMapping()
    {
        // Map from WarehouseEntity to Warehouse (concrete class)
        CreateMap<WarehouseEntity, Warehouse>()
            .ForMember(dest => dest.WarehouseId, opt => opt.MapFrom(src => src.WarehouseId))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.WarehouseLocation))
            .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.WarehouseCapacity))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"Warehouse {src.WarehouseId}"));

        // Map from Warehouse to IWarehouse
        CreateMap<Warehouse, IWarehouse>().ConvertUsing(src => src); // Use Warehouse as the implementation

        CreateMap<Warehouse, WarehouseResponseDto>();
        CreateMap<Item, ItemDto>();
        CreateMap<AddItemToWarehouseDTO, Item>();
        CreateMap<Branch, BranchDto>().ReverseMap();
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<Supplier, SupplierDto>().ReverseMap();
        CreateMap<SupplierOrder, SupplierOrderDto>().ReverseMap();
        CreateMap<SupplierOrder, SupplierOrderDto>().ReverseMap();
        CreateMap<OrderItemRequest, OrderItemRequestDto>();
        CreateMap<Item, AddItemToWarehouseDTO>().ReverseMap();
        CreateMap<Branch, BranchDto>().ReverseMap();
        CreateMap<Item, AddItemToWarehouseDTO>().ReverseMap();

        // Map from ItemEntity to Item
        CreateMap<ItemEntity, Item>()
            .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.Sku))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ItemName))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ItemDescription))
            .ForMember(dest => dest.OriginalStock, opt => opt.MapFrom(src => src.OriginalStock))
            .ForMember(dest => dest.CurrentStock, opt => opt.MapFrom(src => src.CurrentStock))
            .ForMember(dest => dest.ItemCurrency, opt => opt.MapFrom(src => src.ItemCurrency))
            .ForMember(dest => dest.UnitCost, opt => opt.MapFrom(src => src.ItemUnitCost))
            .ForMember(dest => dest.MarginGain, opt => opt.MapFrom(src => src.ItemMarginGain))
            .ForMember(dest => dest.ItemDiscount, opt => opt.MapFrom(src => src.ItemDiscount))
            .ForMember(dest => dest.AdditionalTax, opt => opt.MapFrom(src => src.ItemAdditionalTax))
            .ForMember(dest => dest.ItemPrice, opt => opt.MapFrom(src => src.ItemPrice))
            .ForMember(dest => dest.ItemStatus, opt => opt.MapFrom(src => src.ItemStatus))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.ItemImage, opt => opt.MapFrom(src => src.ItemImage));

        // Map from Item to ItemEntity
        CreateMap<Item, ItemEntity>()
            .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.Sku))
            .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ItemDescription, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.OriginalStock, opt => opt.MapFrom(src => src.OriginalStock))
            .ForMember(dest => dest.CurrentStock, opt => opt.MapFrom(src => src.CurrentStock))
            .ForMember(dest => dest.ItemCurrency, opt => opt.MapFrom(src => src.ItemCurrency))
            .ForMember(dest => dest.ItemUnitCost, opt => opt.MapFrom(src => src.UnitCost))
            .ForMember(dest => dest.ItemMarginGain, opt => opt.MapFrom(src => src.MarginGain))
            .ForMember(dest => dest.ItemDiscount, opt => opt.MapFrom(src => src.ItemDiscount))
            .ForMember(dest => dest.ItemAdditionalTax, opt => opt.MapFrom(src => src.AdditionalTax))
            .ForMember(dest => dest.ItemPrice, opt => opt.MapFrom(src => src.ItemPrice))
            .ForMember(dest => dest.ItemStatus, opt => opt.MapFrom(src => src.ItemStatus))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.ItemImage, opt => opt.MapFrom(src => src.ItemImage));

        // Map from AddItemToWarehouseDTO to Item
        CreateMap<AddItemToWarehouseDTO, Item>()
            .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.Sku))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ItemName))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ItemDescription))
            .ForMember(dest => dest.OriginalStock, opt => opt.MapFrom(src => src.OriginalStock))
            .ForMember(dest => dest.CurrentStock, opt => opt.MapFrom(src => src.CurrentStock))
            .ForMember(dest => dest.ItemCurrency, opt => opt.MapFrom(src => src.ItemCurrency))
            .ForMember(dest => dest.UnitCost, opt => opt.MapFrom(src => src.UnitCost))
            .ForMember(dest => dest.MarginGain, opt => opt.MapFrom(src => src.MarginGain))
            .ForMember(dest => dest.ItemDiscount, opt => opt.MapFrom(src => src.ItemDiscount))
            .ForMember(dest => dest.AdditionalTax, opt => opt.MapFrom(src => src.AdditionalTax))
            .ForMember(dest => dest.ItemPrice, opt => opt.MapFrom(src => src.ItemPrice))
            .ForMember(dest => dest.ItemStatus, opt => opt.MapFrom(src => src.ItemStatus))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.ItemImage, opt => opt.MapFrom(src => src.ItemImage));

        CreateMap<Warehouse, WarehouseResponseDto>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.ToList())); // Convert to List


        CreateMap<Item, ItemDto>();



    }
}
