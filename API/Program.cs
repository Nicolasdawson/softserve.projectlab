using API.Models.Logistics.Interfaces;
using API.Models.Logistics;
using API.Services;
using API.Services.Logistics;
using API.Services.WareHouseService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register your services
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<IWarehouse, Warehouse>(); 
builder.Services.AddScoped<IWarehouseService, WarehouseService>(); 

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
