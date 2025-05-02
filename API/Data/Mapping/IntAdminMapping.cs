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
            // Category | DTO ↔ Entity
            // =========================
            CreateMap<CategoryEntity, CategoryDto>()
                .ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());


            // =========================
            // Category | Entity ↔ Domain model
            // =========================
            CreateMap<CategoryEntity, Category>();
            CreateMap<Category, CategoryEntity>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.CatalogCategoryEntities, opt => opt.Ignore());

            // =========================
            // Catalog | DTO ↔ Entity
            // =========================
            CreateMap<CatalogEntity, CatalogDto>()
                .ForMember(dest => dest.CategoryIds, opt => opt.MapFrom(
                    src => src.CatalogCategoryEntities.Select(cc => cc.CategoryId)
                ))
                .ReverseMap()
                .ForMember(dest => dest.CatalogCategoryEntities, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // =========================
            // Catalog | Entity ↔ Domain model
            // =========================
            CreateMap<CatalogEntity, Catalog>();
            CreateMap<Catalog, CatalogEntity>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.CatalogCategoryEntities, opt => opt.Ignore());

            // =========================
            // Item | DTO ↔ Entity
            // =========================
            CreateMap<ItemEntity, ItemDto>()
                .ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // =========================
            // Item | Entity ↔ Domain model
            // =========================
            CreateMap<ItemEntity, Item>();
            CreateMap<Item, ItemEntity>()
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
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.RolePermissionEntities, opt => opt.Ignore());

            // =========================
            // Role | DTO ↔ Entity
            // =========================
            CreateMap<RoleEntity, RoleDto>()
                .IncludeBase<BaseEntity, BaseDto>()
                .ForMember(dest => dest.PermissionIds, opt => opt.MapFrom(
                    src => src.RolePermissionEntities.Select(rp => rp.PermissionId)
                ))
                .ReverseMap()
                .IncludeBase<BaseDto, BaseEntity>();

            // =========================
            // Role | Entity ↔ Domain model
            // =========================
            CreateMap<RoleEntity, Role>();
            CreateMap<Role, RoleEntity>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.RolePermissionEntities, opt => opt.Ignore())
                .ForMember(dest => dest.UserRoleEntities, opt => opt.Ignore());

            // =========================
            // User | DTO ↔ Entity
            // =========================
            CreateMap<UserEntity, UserDto>()
                .IncludeBase<BaseEntity, BaseDto>()
                .ForMember(dest => dest.RoleIds, opt => opt.MapFrom(
                    src => src.UserRoleEntities.Select(ur => ur.RoleId)
                ))
                .ReverseMap()
                .IncludeBase<BaseDto, BaseEntity>()
                .ForMember(dest => dest.UserRoleEntities, opt => opt.Ignore());

            // =========================
            // User | Entity ↔ Domain model
            // =========================
            CreateMap<UserEntity, User>();
            CreateMap<User, UserEntity>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.UserRoleEntities, opt => opt.Ignore());
        }
    }
}
