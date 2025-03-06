using API.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Registra servicios para la lógica de negocio
builder.Services.AddControllers();
builder.Services.AddScoped<IPackageService, PackageService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();