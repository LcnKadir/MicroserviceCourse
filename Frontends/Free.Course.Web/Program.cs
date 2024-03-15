using Free.Course.Web.Handler;
using Free.Course.Web.Models;
using Free.Course.Web.Services;
using Free.Course.Web.Services.Interfaces;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddScoped<ClientCredentialTokenHandler>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAccessTokenManagement();

builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection(nameof(ServiceApiSettings)));
builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection(nameof(ClientSettings)));

builder.Services.AddScoped<ISharedIndetityService, SharedIdentityService>();
builder.Services.AddScoped<IClientCredentialTokenService, ClientCredentialTokenService>();

var serviceApiSetting = builder.Configuration.GetSection(nameof(ServiceApiSettings)).Get<ServiceApiSettings>();

builder.Services.AddHttpClient<IIdentityService, IdentityService>();

builder.Services.AddHttpClient<ICatalogService, CatalogService>(opt=>
{
    opt.BaseAddress = new Uri($"{serviceApiSetting.GatewayBaseUri}/{serviceApiSetting.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IUserService, UserService>(opt =>
{
    opt.BaseAddress = new Uri(serviceApiSetting.IdentityBaseUri);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IPhotoStockService, PhotoStockService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSetting.GatewayBaseUri}/{serviceApiSetting.PhotoStock.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();



//Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Auth/SignIn";
    opt.ExpireTimeSpan = TimeSpan.FromDays(60);
    opt.SlidingExpiration = true; //Let the cookie period be extended when the user logs in. //Kullanýcý giriþ yaptýðý zaman cookie süresi uzatýlsýn.
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
