using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<MyDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("MSSQL"),
                        builder => builder.MigrationsAssembly(typeof(MyDbContext).Assembly.FullName)));
           
            return services;
        }
    }
}
