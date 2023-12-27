
// Acá es donde tenemos todas las opciones de la app, paquetes, todo el middleware, etc



using Microsoft.AspNetCore.Authentication.Cookies; // Para la auternticación
using Microsoft.AspNetCore.Identity; // Para poder inicializar el AddIdentity
using Microsoft.EntityFrameworkCore; // UseSqlServer()
using Tasq.Data; // Añadí picandole a ApplicationDbContext
using Tasq.Interfaces;
// using Tasq.Interfaces;
using Tasq.Models; // Para poder inicializar el AddIdentity
using Tasq.Repository;
// using Tasq.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ISedeRepository, SedeRepository>();
builder.Services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
builder.Services.AddScoped<ITareaRepository, TareaRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Con las interfaces y los Repos que añadamos, debemos de declararlos acá en Program.cs



// Conectar db, en este caso de Sqlite3
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Identificación y usuarios
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>(); // Ésto es para los usuarios
builder.Services.AddMemoryCache(); // Añadir para los usuarios
builder.Services.AddSession(); // Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(); // Ésto nos debería de dejar ver usuarios




var app = builder.Build();

// Seed data
if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    // await Seed.SeedUsersAndRolesAsync(app); // Usuarios y Roles
    // Seed.SeedData(app); // Info a las db
}






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

app.UseSession();
app.UseAuthentication(); // Por cookies
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

