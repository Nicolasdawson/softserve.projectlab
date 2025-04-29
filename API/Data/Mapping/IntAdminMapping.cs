using System.Linq;
using AutoMapper;
using softserve.projectlabs.Shared.DTOs;
using API.Data.Entities;
using API.Models.IntAdmin;

namespace API.Mappings
{
    public class IntAdminMapping : Profile
    {
        public IntAdminMapping()
        {
            // =========================
            // Category
            // =========================
            CreateMap<CategoryEntity, CategoryDto>()
                .ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<CategoryEntity, Category>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.CategoryStatus))
                .ReverseMap()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CategoryStatus, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // =========================
            // Catalog
            // =========================
            CreateMap<CatalogEntity, CatalogDto>()
                .ForMember(dest => dest.CategoryIds, opt => opt.MapFrom(
                    src => src.CatalogCategoryEntities.Select(x => x.CategoryId)
                ))
                .ReverseMap()
                .ForMember(dest => dest.CatalogCategoryEntities, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<CatalogEntity, Catalog>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CatalogId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CatalogName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.CatalogDescription))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.CatalogStatus))
                .ReverseMap()
                .ForMember(dest => dest.CatalogId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CatalogName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CatalogDescription, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CatalogStatus, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.CatalogCategoryEntities, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // =========================
            // Item
            // =========================
            CreateMap<ItemEntity, ItemDto>()
                .ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<ItemEntity, Item>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ItemName))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ItemPrice))
                // ... otros mappeos 1:1 omitidos por brevedad
                .ReverseMap()
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ItemPrice, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // =========================
            // Permission | DTO ↔ Entity
            // =========================
            CreateMap<PermissionEntity, PermissionDto>()
                .IncludeBase<BaseEntity, BaseDto>()
                .ReverseMap()
                .IncludeBase<BaseDto, BaseEntity>();

            // =========================
            // Permission | Entity ↔ Domain model
            // =========================
            CreateMap<PermissionEntity, Permission>();
            CreateMap<Permission, PermissionEntity>()
                // We ignore audit fields and soft delete fields
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // =========================
            // Role | DTO ↔ Entity
            // =========================
            CreateMap<RoleEntity, RoleDto>()
                .IncludeBase<BaseEntity, BaseDto>()
            // extraemos sólo los IDs de permisos para el DTO
            .ForMember(dest => dest.PermissionIds,
                       opt => opt.MapFrom(src => src.RolePermissionEntities
                                                        .Select(rp => rp.PermissionId)))
            .ReverseMap()

            CreateMap<RoleEntity, Role>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.RoleStatus))
                .ReverseMap()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoleStatus, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.RolePermissionEntities, opt => opt.Ignore())
                .ForMember(dest => dest.UserRoleEntities, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // =========================
            // User
            // =========================
            CreateMap<UserEntity, UserDto>()
                .ForMember(dest => dest.RoleIds, opt => opt.MapFrom(
                    src => src.UserRoleEntities.Select(x => x.RoleId)
                ))
                .ReverseMap()
                .ForMember(dest => dest.UserRoleEntities, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UserEntity, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserContactEmail))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.UserFirstName))
                .ReverseMap()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserContactEmail, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.UserRoleEntities, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
        }
    }
}
