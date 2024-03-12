using Free.Course.Web.Models;
using Free.Course.Web.Services;
using Free.Course.Web.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<IIdentityService, IdentityService>();


builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection(nameof(ServiceApiSettings)));
builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection(nameof(ClientSettings)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
