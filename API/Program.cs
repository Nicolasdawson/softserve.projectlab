using API.Data;
using API.Entity;
using API.implementations.Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var conectionString = builder.Configuration.GetConnectionString("SqlServerConnection");

builder.Services.AddTransient<IProductProcessor, ProductProcessor>();

builder.Services.AddDbContext<ProjectlabContext>(
    db => db.UseSqlServer(conectionString));

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

app.UseAuthorization();

app.MapControllers();

app.Run();