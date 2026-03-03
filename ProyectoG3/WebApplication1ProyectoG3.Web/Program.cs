using Microsoft.EntityFrameworkCore;
using ProyectoG3.Application.Interfaces;
using ProyectoG3.Application.Interfaces;
using ProyectoG3.Infrastructure.Services;
using ProyectoG3.Infrastructure.Persistence;
using ProyectoG3.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();


// Placeholder de conexi�n (tu compa lo cambia luego)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ProyectoG3DB"));

builder.Services.AddScoped<IComercioService, ComercioService>();
builder.Services.AddScoped<ISinpeService, SinpeService>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();

app.Run();