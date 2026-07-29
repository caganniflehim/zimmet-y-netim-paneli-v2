using Microsoft.AspNetCore.Identity;
using envanter.Models;
using Microsoft.EntityFrameworkCore;
using envanter.Data;


var builder = WebApplication.CreateBuilder(args);

// Veritabanı bağlantısı
var connectionString = builder.Configuration.GetConnectionString("EnvanterDbContext")
    ?? throw new InvalidOperationException("Connection string 'EnvanterDbContext' not found.");

builder.Services.AddDbContext<EnvanterDbContext>(options =>
    options.UseSqlServer(connectionString));

// --- YENİ EKLENEN KISIM: Identity (Kullanıcı ve Giriş) Servisleri ---
builder.Services.AddDefaultIdentity<envanter.Data.ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<EnvanterDbContext>();
/*builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<EnvanterDbContext>();*/
// -----------------------------------------------------------------

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// --- YENİ EKLENEN KISIM: Kimlik Doğrulama (Authentication) ---
// Not: UseAuthentication, UseAuthorization'dan HEMEN ÖNCE yazılmalıdır!
app.UseAuthentication();
app.UseAuthorization();
// -------------------------------------------------------------

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Personel}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapRazorPages();
app.Run();