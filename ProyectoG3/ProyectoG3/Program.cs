using ProyectoG3.Repository;
using ProyectoG3.Services;
using ProyectoG3.Services.ProyectoSinpe.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICajaService, CajaService>();
builder.Services.AddScoped<IBitacoraService, BitacoraService>();

// Registro de Repositorios
builder.Services.AddScoped<ICajaRepository, CajaRepository>();
builder.Services.AddScoped<IBitacoraRepository, BitacoraRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();