using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using API.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using API.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar Newtonsoft.Json a la serialización
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    });

// Agregar el servicio ProductService
builder.Services.AddSingleton<ProductService>();

// Configuración de Swagger
builder.Services.AddSwaggerGen(c =>
{
    // Aquí puedes agregar cualquier configuración extra para Swagger si lo necesitas
});

var app = builder.Build();

// Habilitar el uso de Swagger en la API
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.MapControllers();
app.Run();
