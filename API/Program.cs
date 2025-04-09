using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using API.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using API.Services;
using API.implementations.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddControllers();

// Agregar Swagger
builder.Services.AddSwaggerGen();  // Este es el servicio que habilita Swagger en tu API

// Agregar el servicio ProductService
builder.Services.AddScoped<ProductService>();

builder.Services.AddScoped<PaymentService>();

builder.Services.AddScoped<StripePaymentService>();

// Configuración de Swagger
builder.Services.AddSwaggerGen(c =>
{
    // Aquí puedes agregar cualquier configuración extra para Swagger si lo necesitas
});

Stripe.StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

var app = builder.Build();

// Aplicar las migraciones autom�ticamente al iniciar la aplicaci�n
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();  // Esto crear� las tablas si no existen
}


// Habilitar el uso de Swagger en la API
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Habilita el middleware Swagger
    app.UseSwaggerUI();  // Habilita la UI de Swagger
}

// Usar la pol�tica de CORS
app.UseCors("AllowAnyOrigin");

// Configurar los controladores
app.MapControllers();
app.Run();
