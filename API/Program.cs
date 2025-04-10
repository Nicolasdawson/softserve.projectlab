using API.Models.Logistics.Interfaces;
using API.Models.Logistics;
using API.Services;
using API.Services.Logistics;
using API.Services.OrderService;
using Logistics.Models;
using API.Services.Interfaces;
using API.Services.IntAdmin;
using API.Implementations.Domain;
using API.Domain.Logistics;
using API.Utils.Extensions;
using Microsoft.EntityFrameworkCore;
using API.Data.Mapping;
using API.Data;
using softserve.projectlabs.Shared.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Components.Web;
using System.Text.Json.Serialization;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllers()
       .AddJsonOptions(options =>
       {
           options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
           options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
       });

// Add services to the container using the extension method
//builder.Services.AddCustomerServices();
builder.Services.AddWarehouseServices();
builder.Services.AddBranchServices();
builder.Services.AddOrderServices();
builder.Services.AddSupplierServices();
builder.Services.AddSupplierOrderServices();

//// Register your services
builder.Services.AddScoped<API.Implementations.Domain.CustomerDomain>();
builder.Services.AddScoped<API.Services.Interfaces.ICustomerService, API.Services.CustomerService>();
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
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<LogisticsMapping>();
    cfg.AddProfile<CustomerMapping>();
});

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenLocalhost(7153, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7153");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    // Required for development with self-signed cert
    ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
});

builder.WebHost.UseUrls("https://localhost:7153", "http://localhost:5153");

var corsOrigins = builder.Environment.IsDevelopment()
    ? new[] { "https://localhost:7135", "http://localhost:5168" }
    : new[] { "your-production-url" };

var frontendOrigin = "https://localhost:7035";
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(frontendOrigin)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
    options.Secure = CookieSecurePolicy.Always; // Requires HTTPS
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Use(async (context, next) =>
{
    Console.WriteLine($"Incoming request: {context.Request.Path}");
    await next();
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseCookiePolicy();
app.UseCors("AllowFrontend");
app.UseAuthentication(); 
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
}

app.Run();
