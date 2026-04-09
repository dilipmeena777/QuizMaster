using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Services
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔥 Session (Build se pehle)
builder.Services.AddSession();


// ❗ Build yaha hona chahiye
var app = builder.Build();


// Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 🔥 Session use
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();