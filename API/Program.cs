using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using API.Controllers;
using API.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios para controllers
builder.Services.AddControllers();

// Agregar Swagger
builder.Services.AddSwaggerGen(c =>
{
    // Configura Swagger para incluir comentarios XML si lo tienes (opcional)
    // c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "API.xml"));
});

// Agregar el servicio ProductService
builder.Services.AddSingleton<ProductService>();

var app = builder.Build();

// Habilitar el uso de Swagger en la API
if (app.Environment.IsDevelopment())  // Solo habilitar Swagger en desarrollo
{
    app.UseSwagger();  // Habilita el middleware Swagger
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");  // Especifica el endpoint de Swagger
        c.RoutePrefix = string.Empty;  // Configura el Swagger UI para estar disponible en la raíz (opcional)
    });
}

// Configurar los controladores
app.MapControllers();

app.Run();
