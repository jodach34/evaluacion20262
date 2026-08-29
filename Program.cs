using Microsoft.EntityFrameworkCore;
using TecnoGasHogar.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuración opcional para evitar el límite de inotify en contenedores Linux como Render
if (!builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
    builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: false);
}

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuración de SQLite con EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Solicitudes}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();