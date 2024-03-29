﻿using Application.Abstracts.Common.Interfaces;
using Application.Blogs.Commands.CreateBlog;
using Domain.Entities.Membership;
using FluentValidation;
using Infrastructure.Concretes.Common;
using Infrastructure.Identity.Providers;
using Infrastructure.Persistance;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        services.AddScoped<UserManager<AppUser>>();
        services.AddScoped<SignInManager<AppUser>>();
        services.AddScoped<RoleManager<AppRole>>();

        services.AddScoped<IYelloadDbContext>(provider => provider.GetRequiredService<YelloadDbContext>());
        services.AddDbContext<YelloadDbContext>(options =>
             options.UseNpgsql(configuration.GetConnectionString("cString")));
        services.AddIdentity<AppUser, AppRole>()
                        .AddEntityFrameworkStores<YelloadDbContext>()
                        .AddDefaultTokenProviders()
                        .AddErrorDescriber<YelloadIdentityErrorDescriber>();
        services.AddScoped<IValidator<CreateBlogCommand>, CreateBlogCommandValidator>();

        services.AddAuthentication(cfg =>
        {
            cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["jwt:issuer"],
                ValidAudience = configuration["jwt:audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"])),
                ClockSkew = TimeSpan.FromDays(1),
                LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                {
                    return expires >= DateTime.UtcNow;
                }
            };
        });
        services.Configure<CryptoServiceOptions>(cfg =>
        {
            configuration.GetSection("cryptograpy").Bind(cfg);
        });
        services.AddSingleton<ICryptoService, CryptoService>();

        services.Configure<EmailServiceOptions>(cfg =>
        {
            configuration.GetSection("emailAccount").Bind(cfg);
        });
        services.AddSingleton<IEmailService, EmailService>();

        services.Configure<TokenServiceOptions>(cfg =>
        {
            configuration.GetSection("jwt").Bind(cfg);
            cfg.DurationMinutes = (int)TimeSpan.FromHours(3).TotalMinutes;
        });
        services.AddSingleton<ITokenService, TokenService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IClaimsTransformation, AppClaimProvider>();
        
        return services;
    }
}
