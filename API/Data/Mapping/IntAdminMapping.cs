using System.Linq;
using AutoMapper;
using softserve.projectlabs.Shared.DTOs;
using API.Data.Entities;
using API.Models.IntAdmin;
using softserve.projectlabs.Shared.DTOs.Catalog;
using softserve.projectlabs.Shared.DTOs.Category;
using softserve.projectlabs.Shared.DTOs.User;

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
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore()) // 🔒 no sobreescribimos la PK
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.CatalogCategoryEntities, opt => opt.Ignore());

            // =========================
            // Category | Entity ↔ Domain model
            // =========================
            CreateMap<CategoryEntity, Category>();
            CreateMap<Category, CategoryEntity>()
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore()) // ⚠️ evitar conflictos en Update
                .ForMember(dest => dest.CatalogCategoryEntities, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // =========================
            // Category | DTO ↔ Domain
            // =========================
            CreateMap<CategoryDto, Category>().ReverseMap();

            // Create ↔ Domain
            CreateMap<CategoryCreateDto, Category>();

            // Update ↔ Domain
            CreateMap<CategoryUpdateDto, Category>();

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
            // CatalogCategoryEntity → Category (usado en Catalog → Domain)
            // =========================
            CreateMap<CatalogCategoryEntity, Category>()
                .ConvertUsing(src => new Category
                {
                    CategoryId = src.Category.CategoryId,
                    CategoryName = src.Category.CategoryName,
                    CategoryStatus = src.Category.CategoryStatus
                });

            // =========================
            // Catalog | Entity ↔ Domain model
            // =========================
            CreateMap<CatalogEntity, Catalog>()
    .ForMember(dest => dest.Categories, opt => opt.MapFrom(src =>
        src.CatalogCategoryEntities
            .Where(cc => cc.Category != null && !cc.Category.IsDeleted)
            .Select(cc => new Category  // <--- aquí el mapeo manual
            {
                CategoryId = cc.Category.CategoryId,
                CategoryName = cc.Category.CategoryName,
                CategoryStatus = cc.Category.CategoryStatus
            })
    ));


            CreateMap<Catalog, CatalogEntity>()
                .ForMember(dest => dest.CatalogId, opt => opt.Ignore())
                .ForMember(dest => dest.CatalogCategoryEntities, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // =========================
            // Catalog | DTO ↔ Domain
            // =========================
            CreateMap<CatalogDto, Catalog>().ReverseMap();

            // Create ↔ Domain
            CreateMap<CatalogCreateDto, Catalog>()
                .ForMember(dest => dest.Categories, opt => opt.Ignore()); // se cargan desde CategoryIds

            // Update ↔ Domain
            CreateMap<CatalogUpdateDto, Catalog>()
                .ForMember(dest => dest.Categories, opt => opt.Ignore());

            // Read (Domain → DTO)
            CreateMap<Catalog, CatalogDto>()
                .ForMember(dest => dest.CategoryIds, opt => opt.MapFrom(
                    src => src.Categories.Select(c => c.CategoryId)
                ));

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
                .ForMember(dest => dest.UserRoleEntities, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // =========================
            // User | Entity ↔ Domain model
            // =========================
            CreateMap<UserEntity, User>()
                .ReverseMap()
                .ForMember(dest => dest.UserRoleEntities, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            // =========================
            // User | DTO ↔ Domain
            // =========================
            CreateMap<UserDto, User>().ReverseMap();

            // =========================
            // Create ↔ Domain
            // =========================
            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // hash se genera en el Domain
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
                .ForMember(dest => dest.Roles, opt => opt.Ignore()); // se cargan desde RoleIds

            // =========================
            // Update ↔ Domain
            // =========================
            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
        }
    }
}
