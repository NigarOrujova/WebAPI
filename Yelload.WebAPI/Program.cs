using Application;
using Application.Extensions;
using FluentValidation;
using Infrastructure;
using Infrastructure.Identity.Providers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(cfg => {
    cfg.AddPolicy("allowAll", p =>
    {
        p.WithOrigins("https://yelload.com", "https://admin.yelload.com", "http://209.38.196.194:3000", "http://209.38.196.194", "https://cms.yelload.com", "http://164.90.216.81:7228", "http://164.90.216.81")
         .AllowAnyMethod()
         .AllowAnyHeader()
         .AllowCredentials();
    });
});
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

var assemblies = AppDomain.CurrentDomain
    .GetAssemblies()
    .ToArray();
builder.Services.AddMediatR(assemblies);
var types = typeof(Program).Assembly.GetTypes();
builder.Services.AddValidatorsFromAssemblies(assemblies, ServiceLifetime.Singleton);
AppClaimProvider.principals = types
    .Where(t => typeof(ControllerBase).IsAssignableFrom(t) && t.IsDefined(typeof(AuthorizeAttribute), true))
    .SelectMany(t => t.GetCustomAttributes<AuthorizeAttribute>())
    .Union(
    types
    .Where(t => typeof(ControllerBase).IsAssignableFrom(t))
    .SelectMany(type => type.GetMethods())
    .Where(method => method.IsPublic
     && !method.IsDefined(typeof(NonActionAttribute), true)
     && method.IsDefined(typeof(AuthorizeAttribute), true))
     .SelectMany(t => t.GetCustomAttributes<AuthorizeAttribute>())
    )
    .Where(a => !string.IsNullOrWhiteSpace(a.Policy))
    .SelectMany(a => a.Policy.Split(new[] { "," }, System.StringSplitOptions.RemoveEmptyEntries))
.Distinct()
.ToArray();

builder.Services.AddAuthorization(cfg =>
{

    foreach (string principal in AppClaimProvider.principals)
    {
        cfg.AddPolicy(principal, p =>
        {
            p.RequireAssertion(handler =>
            {
                return handler.User.HasAccess(principal);

            });
        });
    }
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Images")),
    RequestPath = "/Images"
});
app.UseCors("allowAll");
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
