using Application;
using FluentValidation;
using Infrastructure;
using Infrastructure.Identity.Providers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

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


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("allowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
