using Application.Abstracts.Common.Interfaces;
using Infrastructure.Concretes.Common;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IYelloadDbContext>(provider => provider.GetRequiredService<YelloadDbContext>());
        services.AddDbContext<YelloadDbContext>(options =>
             options.UseNpgsql(configuration.GetConnectionString("cString")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
