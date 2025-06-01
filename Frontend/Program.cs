using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Frontend;
using MudBlazor.Services;
using Frontend.Services;
using Frontend.AuthProvider;
using Frontend.Repositories;
using Frontend.Shared;
using CurrieTechnologies.Razor.SweetAlert2;



// we are going to work in the frontend in future weeks 

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var urlProd = "";
var urlLocal = "https://localhost:50309";
//Register Services
builder.Services.AddSingleton<CartState>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri( urlLocal /*builder.HostEnvironment.BaseAddress*/) });
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddSweetAlert2();
builder.Services.AddMudServices();
builder.Services.AddAuthorizationCore();
builder.Services.AddHttpClient();

builder.Services.AddScoped<AuthenticationProviderJWT>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());
builder.Services.AddScoped<ILoginService, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());

await builder.Build().RunAsync();