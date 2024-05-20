using CongestionTaxCalculator.Api.Common.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CongestionTaxCalculator.Api.Common.Extensions;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection ConfigureDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(x =>
            x.UseSqlite(configuration.GetConnectionString(ApiConstants.DefaultConnection)));
        return services;
    }

    public static IServiceCollection ConfigureValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

        return services;
    }
}