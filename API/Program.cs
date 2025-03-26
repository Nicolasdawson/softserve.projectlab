using API.Models.Logistics.Interfaces;
using API.Models.Logistics;
using API.Services;
using API.Services.Logistics;
using API.Services.OrderService;
using Logistics.Models;
using API.Services.Interfaces;
using API.Services.IntAdmin; 
using API.Implementations.Domain;
using API.implementations.Domain;
using API.Domain.Logistics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllers();

// Register your services
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<PackagesDomain>();
builder.Services.AddScoped<IWarehouse, Warehouse>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IBranch, Branch>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrder, Order>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<ISupplier, Supplier>();
builder.Services.AddScoped<BranchDomain>();
builder.Services.AddScoped<OrderDomain>();
builder.Services.AddScoped<SupplierDomain>();
builder.Services.AddScoped<SupplierOrderDomain>();
builder.Services.AddScoped<WarehouseDomain>();
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
builder.Services.AddScoped<ISupplierOrderService, SupplierOrderService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
