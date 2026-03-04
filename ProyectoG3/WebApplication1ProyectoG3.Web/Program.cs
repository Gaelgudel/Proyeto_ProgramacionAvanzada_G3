using Microsoft.EntityFrameworkCore;
using ProyectoG3.Application.Interfaces;
using ProyectoG3.Infrastructure.Persistence;
using ProyectoG3.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddScoped<IComercioService, ComercioService>();
builder.Services.AddScoped<ICajaService, CajaService>();


var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();

app.Run();