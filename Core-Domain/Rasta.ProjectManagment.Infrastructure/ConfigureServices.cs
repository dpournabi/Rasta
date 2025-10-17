using Rasta.ProjectManagment.Application.Common.Interfaces;
using Rasta.ProjectManagment.Infrastructure.Persistence;
using Rasta.ProjectManagment.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Rasta.ProjectManagment.Infrastructure.Persistence.Interceptors;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        services.AddDbContext<Rasta.ProjectManagment.Infrastructure.Persistence.ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MSSQL"),
                    builder => builder.MigrationsAssembly(typeof(Rasta.ProjectManagment.Infrastructure.Persistence.ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<ApplicationDbContextInitialiser>();
        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddScoped<IStringHelperService, StringHelperService>();

        services.AddAuthorization(options =>
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));
        return services;
    }
}
