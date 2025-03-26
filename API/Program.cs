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
              .AllowAnyMethod()  // Permite cualquier método HTTP (GET, POST, etc.)
              .AllowAnyHeader();  // Permite cualquier encabezado
    });
});

// Agregar servicios para controllers
builder.Services.AddControllers();

// Agregar Swagger
builder.Services.AddSwaggerGen();  // Este es el servicio que habilita Swagger en tu API

// Agregar el servicio ProductService
builder.Services.AddScoped<ProductService>();

// ConfiguraciÃ³n de Swagger
builder.Services.AddSwaggerGen(c =>
{
    // AquÃ­ puedes agregar cualquier configuraciÃ³n extra para Swagger si lo necesitas
});

var app = builder.Build();

// Habilitar el uso de Swagger en la API
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Habilita el middleware Swagger
    app.UseSwaggerUI();  // Habilita la UI de Swagger
}

// Usar la política de CORS
app.UseCors("AllowAnyOrigin");

// Configurar los controladores
app.MapControllers();
app.Run();
