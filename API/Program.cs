
using API.Data;
using API.Repository.IRepository;
using API.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using API.Mapping;

var builder = WebApplication.CreateBuilder(args);

//Add dbContext service
builder.Services.AddDbContext<DbAb6d2eProjectlabContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionSql")));

// Add Repositories
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(ClientMapper));
builder.Services.AddAutoMapper(typeof(ProductCategoryMapper));
builder.Services.AddAutoMapper(typeof(ProductMapper));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();

app.Run();

