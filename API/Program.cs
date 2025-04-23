using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using API.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using API.Services;
using API.Helpers;
using API.implementations.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Configurar Entity Framework con SQL Server
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<SeedDb>();//Se registra SeedDb como servicio transitorio para poder usarse
builder.Services.AddScoped<IFileStorage, FileStorage>();



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
builder.Services.AddScoped<ProductImageService>();

// Conexión con Azure blob DB Azure
builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration["ConnectionStrings:AzureStorage:blob"]!, preferMsi: true);
    clientBuilder.AddQueueServiceClient(builder.Configuration["ConnectionStrings:AzureStorage:queue"]!, preferMsi: true);
});

builder.Services.AddScoped<PaymentService>();

builder.Services.AddScoped<StripePaymentService>();

builder.Services.AddScoped<EmailService>();


// Configuración de Swagger
builder.Services.AddSwaggerGen(c =>
{
    // Aquí puedes agregar cualquier configuración extra para Swagger si lo necesitas
});

Stripe.StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

var app = builder.Build();
SeedData(app);

void SeedData(WebApplication App)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using var scope = scopedFactory!.CreateScope();
    var service = scope.ServiceProvider.GetService<SeedDb>();
    service!.SeedAsync().Wait();
}

/*
 
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
        
                                 //
                                 //dbContext.Database.Migrate(); // Aplica las migraciones si es necesario
                                 //dbContext.Database.EnsureCreated();// Si quieres asegurarte de que se creen las tablas antes de poblar datos
        
     
                                 //dbContext.Database.EnsureDeleted(); // Elimina la base de datos
                                 //dbContext.Database.EnsureCreated();  // Esto crear� las tablas si no existen
        
}
*/

// Habilitar el uso de Swagger en la API
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Habilita el middleware Swagger
    app.UseSwaggerUI();  // Habilita la UI de Swagger
}

app.UseStaticFiles(new StaticFileOptions { 
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Images\\Products")),
    RequestPath= "/Images"
});

/*
builder.Services.AddDirectoryBrowser();

var fileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Images\\Products"));
var requestPath = "/MyImages";

// Enable displaying browser links.
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = fileProvider,
    RequestPath = requestPath
});

app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = fileProvider,
    RequestPath = requestPath
});
*/

// Usar la pol�tica de CORS
app.UseCors("AllowAnyOrigin");

// Configurar los controladores
app.MapControllers();
app.MapDefaultControllerRoute();  // Configurar la ruta predeterminada para las vistas

app.Run();
