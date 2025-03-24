using API.Models.Logistics.Interfaces;
using API.Models.Logistics;
using API.Services;
using API.Services.Logistics;
using API.Services.WareHouseService;
<<<<<<< HEAD
using API.Services.OrderService;
using Logistics.Models;
=======
using API.Services.IntAdmin; // Asegúrate de incluir el namespace correcto para CatalogService
using API.Implementations.Domain; // Para CatalogDomain
>>>>>>> 9e55238e63d8aeb7a24c02abb3085a3edf5128f5

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register your services
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<IWarehouse, Warehouse>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IBranch, Branch>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrder, Order>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
<<<<<<< HEAD
builder.Services.AddScoped<ISupplier, Supplier>();
=======
builder.Services.AddScoped<CatalogDomain>();
builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<CategoryDomain>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ItemDomain>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<PermissionDomain>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<RoleDomain>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<UserDomain>();
builder.Services.AddScoped<IUserService, UserService>();
>>>>>>> 9e55238e63d8aeb7a24c02abb3085a3edf5128f5

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
