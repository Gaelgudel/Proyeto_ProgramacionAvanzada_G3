using ProyectoG3.Repository;
using ProyectoG3.Services;
using ProyectoG3.Services.ProyectoSinpe.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICajaService, CajaService>();
// Registro de Servicios
builder.Services.AddScoped<IBitacoraService, BitacoraService>();

// Registro de Repositorios (Debes crear las implementaciones de estos)
builder.Services.AddScoped<ICajaRepository, CajaRepository>();
builder.Services.AddScoped<IBitacoraRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
