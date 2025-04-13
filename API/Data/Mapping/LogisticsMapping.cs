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
        CreateMap<WarehouseEntity, Warehouse>()
            .ForMember(dest => dest.WarehouseId, opt => opt.MapFrom(src => src.WarehouseId))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.WarehouseLocation))
            .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.WarehouseCapacity))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"Warehouse {src.WarehouseId}"));

        // Map from Warehouse to IWarehouse
        CreateMap<Warehouse, IWarehouse>().ConvertUsing(src => src); // Use Warehouse as the implementation

        CreateMap<Warehouse, WarehouseResponseDto>();
        CreateMap<Item, ItemDto>();

        // Consolidated mapping for AddItemToWarehouseDTO to Item
        CreateMap<AddItemToWarehouseDTO, Item>()
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
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Ignore as it will be set in the domain
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()) // Ignore as it will be set in the domain
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore()); // Ignore as it will be set in the domain

        // Explicit mapping from BranchEntity to Branch
        CreateMap<BranchEntity, Branch>()
            .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.BranchName))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.BranchCity))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.BranchAddress))
            .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.BranchRegion))
            .ForMember(dest => dest.ContactNumber, opt => opt.MapFrom(src => src.BranchContactNumber))
            .ForMember(dest => dest.ContactEmail, opt => opt.MapFrom(src => src.BranchContactEmail));

        // Explicit mapping from Branch to BranchEntity
        CreateMap<Branch, BranchEntity>()
            .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.BranchCity, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.BranchAddress, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.BranchRegion, opt => opt.MapFrom(src => src.Region))
            .ForMember(dest => dest.BranchContactNumber, opt => opt.MapFrom(src => src.ContactNumber))
            .ForMember(dest => dest.BranchContactEmail, opt => opt.MapFrom(src => src.ContactEmail))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Ignore as it will be set in the domain
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()) // Ignore as it will be set in the domain
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore()); // Ignore as it will be set in the domain

        // Consolidated mapping for SupplierOrder to SupplierOrderDto
        CreateMap<SupplierOrder, SupplierOrderDto>().ReverseMap();

        CreateMap<WarehouseDto, WarehouseEntity>().ReverseMap();
        CreateMap<Warehouse, WarehouseResponseDto>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.ToList())); // Convert to List
    }
}

