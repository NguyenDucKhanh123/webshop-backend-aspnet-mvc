using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebShopApp.Data;
using WebShopApp.Models;
using WebShopApp.Service;

var builder = WebApplication.CreateBuilder(args);

// ✅ Kết nối CSDL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// ✅ Identity: chỉ gọi 1 lần, dùng ApplicationUser và thêm vai trò
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Có thể bật true nếu dùng xác thực email
})
.AddRoles<IdentityRole>() // Nếu có phân quyền theo vai trò
.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<CartService>();

// ✅ Bổ sung Razor Pages và MVC Controller
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// ✅ Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // ❗ Bắt buộc nếu dùng Identity
app.UseAuthorization();

// ✅ Định tuyến
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
