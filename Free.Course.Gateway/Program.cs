using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"configuration.{builder.Environment.EnvironmentName.ToLower()}.json");



builder.Services.AddOcelot();


var app = builder.Build();
await app.UseOcelot();
app.Run();
