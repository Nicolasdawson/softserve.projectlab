using AutoMapper;
using API.Data.Entities;
using API.Models.Logistics;
using API.Models.Logistics.Interfaces;
using API.Models.IntAdmin;
using Logistics.Models;

public class LogisticsMapping : Profile
{
    public LogisticsMapping()
    {
        // Map from WarehouseEntity to Warehouse (concrete class)
        CreateMap<WarehouseEntity, Warehouse>()
            .ForMember(dest => dest.WarehouseId, opt => opt.MapFrom(src => src.WarehouseId))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.WarehouseLocation));
        // Add other property mappings here

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


        // Map from SupplierEntity to Supplier
        CreateMap<SupplierEntity, Supplier>()
            .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId))
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.SupplierName))
            .ForMember(dest => dest.SupplierAddress, opt => opt.MapFrom(src => src.SupplierAddress))
            .ForMember(dest => dest.SupplierContactNumber, opt => opt.MapFrom(src => src.SupplierContactNumber))
            .ForMember(dest => dest.SupplierContactEmail, opt => opt.MapFrom(src => src.SupplierContactEmail))
            .ForMember(dest => dest.ProductsSupplied, opt => opt.Ignore()) 
            .ForMember(dest => dest.IsActive, opt => opt.Ignore()) 
            .ForMember(dest => dest.Orders, opt => opt.Ignore()); 

        // Map from Supplier to SupplierEntity
        CreateMap<Supplier, SupplierEntity>()
            .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId))
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.SupplierName))
            .ForMember(dest => dest.SupplierAddress, opt => opt.MapFrom(src => src.SupplierAddress))
            .ForMember(dest => dest.SupplierContactNumber, opt => opt.MapFrom(src => src.SupplierContactNumber))
            .ForMember(dest => dest.SupplierContactEmail, opt => opt.MapFrom(src => src.SupplierContactEmail));


        // Order mappings
        CreateMap<OrderEntity, Order>()
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.OrderStatus))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.OrderTotalAmount))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItemEntities.Select(oi => new OrderItemResponse
            {
                Sku = oi.Sku,
                Quantity = oi.ItemQuantity,
                Name = oi.SkuNavigation.ItemName,
                UnitPrice = oi.SkuNavigation.ItemPrice
            })));

        CreateMap<OrderItemRequest, OrderEntity>()
            .ForMember(dest => dest.OrderId, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(_ => "Pending"))
            .ForMember(dest => dest.OrderTotalAmount, opt => opt.Ignore()) // Will be calculated
            .ForMember(dest => dest.OrderItemEntities, opt => opt.MapFrom(src => src.Sku));

        // Order item mappings
        CreateMap<OrderItemRequest, OrderItemEntity>()
            .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.Sku))
            .ForMember(dest => dest.ItemQuantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.OrderId, opt => opt.Ignore())
            .ForMember(dest => dest.SkuNavigation, opt => opt.Ignore());

        CreateMap<OrderItemEntity, OrderItemResponse>()
            .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.Sku))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.ItemQuantity))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SkuNavigation.ItemName))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.SkuNavigation.ItemPrice));
    }
}

