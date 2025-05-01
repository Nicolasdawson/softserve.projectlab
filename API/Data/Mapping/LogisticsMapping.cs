using API.Data.Entities;
using API.Models.IntAdmin;
using API.Models.Logistics.Interfaces;
using API.Models.Logistics;
using AutoMapper;
using softserve.projectlabs.Shared.DTOs;

public class LogisticsMapping : Profile
{
    public LogisticsMapping()
    {
        // Map from WarehouseEntity to Warehouse (concrete class)
        // Once I figure out Automapping I will add this again.
        //CreateMap<WarehouseEntity, Warehouse>()
        //    .ForMember(dest => dest.WarehouseId, opt => opt.MapFrom(src => src.WarehouseId))
        //    .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.WarehouseLocation))
        //    .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.WarehouseCapacity))
        //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"Warehouse {src.WarehouseId}"))
        //    .ForMember(dest => dest.Items, opt => opt.Ignore()); // Ensure Items are handled manually if needed


        // Map from Warehouse to IWarehouse
        CreateMap<Warehouse, IWarehouse>().ConvertUsing(src => src); // Use Warehouse as the implementation

        CreateMap<Warehouse, WarehouseResponseDto>();
        CreateMap<Item, ItemDto>()
            .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.Sku))
            .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.ItemName))
            .ForMember(dest => dest.ItemDescription, opt => opt.MapFrom(src => src.ItemDescription))
            .ForMember(dest => dest.ItemPrice, opt => opt.MapFrom(src => src.ItemPrice))
            .ForMember(dest => dest.CurrentStock, opt => opt.MapFrom(src => src.CurrentStock));


        // Consolidated mapping for AddItemToWarehouseDTO to Item
        CreateMap<AddItemToWarehouseDto, Item>()
            .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.Sku))
            .ReverseMap();

        // Explicit mapping from BranchEntity to BranchDto
        CreateMap<BranchEntity, BranchDto>()
            .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.BranchName))
            .ForMember(dest => dest.BranchCity, opt => opt.MapFrom(src => src.BranchCity))
            .ForMember(dest => dest.BranchAddress, opt => opt.MapFrom(src => src.BranchAddress))
            .ForMember(dest => dest.BranchRegion, opt => opt.MapFrom(src => src.BranchRegion))
            .ForMember(dest => dest.BranchContactNumber, opt => opt.MapFrom(src => src.BranchContactNumber))
            .ForMember(dest => dest.BranchContactEmail, opt => opt.MapFrom(src => src.BranchContactEmail))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));


        // Explicit mapping from BranchDto to BranchEntity
        CreateMap<BranchDto, BranchEntity>()
            .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.BranchName))
            .ForMember(dest => dest.BranchCity, opt => opt.MapFrom(src => src.BranchCity))
            .ForMember(dest => dest.BranchAddress, opt => opt.MapFrom(src => src.BranchAddress))
            .ForMember(dest => dest.BranchRegion, opt => opt.MapFrom(src => src.BranchRegion))
            .ForMember(dest => dest.BranchContactNumber, opt => opt.MapFrom(src => src.BranchContactNumber))
            .ForMember(dest => dest.BranchContactEmail, opt => opt.MapFrom(src => src.BranchContactEmail))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) 
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()) 
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        // Explicit mapping from BranchEntity to Branch
        // Once I figure out Automapping I will add this again.
        //CreateMap<BranchEntity, Branch>() 
        //    .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
        //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.BranchName))
        //    .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.BranchCity))
        //    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.BranchAddress))
        //    .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.BranchRegion))
        //    .ForMember(dest => dest.ContactNumber, opt => opt.MapFrom(src => src.BranchContactNumber))
        //    .ForMember(dest => dest.ContactEmail, opt => opt.MapFrom(src => src.BranchContactEmail));

        // Explicit mapping from Branch to BranchEntity
        // Once I figure out Automapping I will add this again.
        //CreateMap<Branch, BranchEntity>()
        //    .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
        //    .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Name))
        //    .ForMember(dest => dest.BranchCity, opt => opt.MapFrom(src => src.City))
        //    .ForMember(dest => dest.BranchAddress, opt => opt.MapFrom(src => src.Address))
        //    .ForMember(dest => dest.BranchRegion, opt => opt.MapFrom(src => src.Region))
        //    .ForMember(dest => dest.BranchContactNumber, opt => opt.MapFrom(src => src.ContactNumber))
        //    .ForMember(dest => dest.BranchContactEmail, opt => opt.MapFrom(src => src.ContactEmail))
        //    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) 
        //    .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()) 
        //    .ForMember(dest => dest.IsDeleted, opt => opt.Ignore()); 


        // Once I figure out Automapping I will add this again.
        //CreateMap<SupplierOrder, SupplierOrderDto>()
        //    .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
        //    .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId))
        //    .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
        //    .ForMember(dest => dest.ExpectedDeliveryDate, opt => opt.MapFrom(src => src.ExpectedDeliveryDate))
        //    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
        //    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        // Add mapping for SupplierDto to SupplierEntity
        CreateMap<SupplierDto, SupplierEntity>()
            .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId))
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.SupplierAddress, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.SupplierContactNumber, opt => opt.MapFrom(src => src.ContactNumber))
            .ForMember(dest => dest.SupplierContactEmail, opt => opt.MapFrom(src => src.ContactEmail))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted == false ? false : src.IsDeleted)) 
            .ReverseMap();


        CreateMap<WarehouseDto, WarehouseEntity>().ReverseMap();
        CreateMap<Warehouse, WarehouseResponseDto>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.ToList())); 
    }
}

