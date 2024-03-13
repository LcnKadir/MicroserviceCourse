using Free.Course.Web.Models;
using Free.Course.Web.Services;
using Free.Course.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddHttpContextAccessor();

builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection(nameof(ServiceApiSettings)));
builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection(nameof(ClientSettings)));

var serviceApiSetting = builder.Configuration.GetSection(nameof(ServiceApiSettings)).Get<ServiceApiSettings>();

builder.Services.AddHttpClient<IIdentityService, IdentityService>();
builder.Services.AddHttpClient<IUserService, UserService>(opt=>
{
    opt.BaseAddress = new Uri(serviceApiSetting.IdentityBaseUri);
});


//Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Auth/SignIn";
    opt.ExpireTimeSpan = TimeSpan.FromDays(60);
    opt.SlidingExpiration = true; //Let the cookie period be extended when the user logs in. //Kullan�c� giri� yapt��� zaman cookie s�resi uzat�ls�n.
    opt.Cookie.Name = "webcookie";
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();