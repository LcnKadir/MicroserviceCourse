using Free.Course.Services.Basket.Settings;
using FreeCourse.Services.Catalog.Services;
using FreeCourse.Services.Catalog.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt => //Authorize has been added to protect all controllers. This way we won't have to pass Authorize on every controller.
                                       //Tüm controller'larý koruma altýna almak için Authorize eklemesi yapýldý. Böylelikle her controller'da  Authorize etribütünü geçmek zorunda kalmayacaðýz.
{
    opt.Filters.Add(new AuthorizeFilter());
}); 

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); // Added AutoMapper to convert DTOs. // DTO'larý dönüþtürmek için AutoMapper'ý eklendi.


builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value; // "GetRequiredService" will throw an error if it cannot find the corresponding service. // "GetRequiredService" eðer ilgili servisi bulamazsa hata fýrlatacak.
});


//JSONWEBTOKEN
//Microservice is protected. // Microservice'i koruma altýna aldýk.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_catalog";
    options.RequireHttpsMetadata = false;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
