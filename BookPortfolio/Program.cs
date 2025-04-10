using BookPortfolio.Data;
using BookPortfolio.Interfaces;
using BookPortfolio.Models;
using BookPortfolio.Repositorys;
using BookPortfolio.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 12;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;


}).AddCookie();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;  //Forces cookies to be sent only over HTTPS
    options.Cookie.SameSite = SameSiteMode.Lax;  // Allows cross-origin authentication if needed
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
});

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Name = "X-CSRF-TOKEN";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.None;
});


builder.Services.AddAuthorization();


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});




builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
builder.Services.AddScoped<IOLService, OLService>();
builder.Services.AddHttpClient<IOLService, OLService>();


var app = builder.Build();


app.UseHttpsRedirection();

app.UseCors(x => x
.AllowAnyMethod()
.AllowAnyHeader()
.AllowCredentials()
.SetIsOriginAllowed(origin => true)
);

app.UseStaticFiles();

app.UseRouting();

app.Use(async (context, next) =>
{
    Console.WriteLine($"Before Authentication: User Authenticated? {context.User.Identity?.IsAuthenticated}");
    await next.Invoke();
    Console.WriteLine($"After Authentication: User Authenticated? {context.User.Identity?.IsAuthenticated}");
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Portfolio",
    pattern: "Portfolio",
    defaults: new { Controller = "Portfolio", action = "Index" }
);

app.MapControllerRoute(
    name: "UserPortfolio",
    pattern: "{username}",
    defaults: new { Controller = "Portfolio", action = "UserPortfolio" }
);

app.MapControllerRoute(
    name: "BookView",
    pattern: "BookView/{bookId}",
    defaults: new { Controller = "Book", action = "BookView" }
    );

app.MapControllers();
app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();
