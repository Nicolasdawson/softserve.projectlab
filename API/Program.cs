using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using API.Controllers;
using API.Services;
using API.implementations.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using API.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar Entity Framework con SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", policy =>
    {
        // Permitir cualquier origen que tenga 'localhost' en la URL
        policy.AllowAnyOrigin()  // Permite cualquier puerto de localhost
              .AllowAnyMethod()  // Permite cualquier m�todo HTTP (GET, POST, etc.)
              .AllowAnyHeader();  // Permite cualquier encabezado
    });
});

// Agregar servicios para controllers
//builder.Services.AddControllers();

// Agregar servicios para controllers y vistas
builder.Services.AddControllersWithViews(); // Aquí es donde permitimos vistas (Razor)
// Agrega servicios de Razor Pages
builder.Services.AddRazorPages();

// Add Swagger
builder.Services.AddSwaggerGen();  // Este es el servicio que habilita Swagger en tu API

// Add service ProductService
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

// Aplicar las migraciones autom�ticamente al iniciar la aplicaci�n
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.ClearDatabase(); // Borra los datos sin eliminar la base de datos

    // Verifica si ya existen categorías para evitar duplicados
    if (!dbContext.Categories.Any())
    {
        // Agregar categorías
        var category1 = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Cámaras de Seguridad",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        var category2 = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Alarmas",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        var category3 = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Sensores",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dbContext.Categories.AddRange(category1, category2, category3);
        dbContext.SaveChanges(); // Guardar las categorías

        // Agregar productos
        dbContext.Products.AddRange(
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Cámara de Seguridad IP 1080p",
                Description = "Cámara de seguridad de alta definición con visión nocturna y grabación en 1080p.",
                Price = 120.99m,
                Weight = 0.5m,
                Height = 10m,
                Width = 15m,
                Length = 20m,
                Stock = 50,
                IdCategory = category1.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Alarma Inalámbrica 4 Zonas",
                Description = "Sistema de alarma inalámbrico con 4 zonas.",
                Price = 150.50m,
                Weight = 1.2m,
                Height = 8m,
                Width = 20m,
                Length = 25m,
                Stock = 100,
                IdCategory = category2.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
    }
    dbContext.SaveChanges(); // Guardar los productos
        /*
                                 //
                                 //dbContext.Database.Migrate(); // Aplica las migraciones si es necesario
                                 //dbContext.Database.EnsureCreated();// Si quieres asegurarte de que se creen las tablas antes de poblar datos
        
     
        dbContext.Database.EnsureDeleted(); // Elimina la base de datos
        dbContext.Database.EnsureCreated();  // Esto crear� las tablas si no existen
         */
}

// Habilitar el uso de Swagger en la API
if (app.Environment.IsDevelopment())  // Solo habilitar Swagger en desarrollo
{
    app.UseSwagger();  // Habilita el middleware Swagger
    app.UseSwaggerUI();  // Habilita la UI de Swagger
}

// Usar la pol�tica de CORS
app.UseCors("AllowAnyOrigin");

// Configurar los controladores
app.MapControllers();
app.MapDefaultControllerRoute();  // Configurar la ruta predeterminada para las vistas

app.Run();
