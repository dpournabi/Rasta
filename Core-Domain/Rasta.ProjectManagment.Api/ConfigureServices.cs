using Rasta.ProjectManagment.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Rasta.ProjectManagment.Application.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Rasta.ProjectManagment.Services;
using Rasta.ProjectManagment.Models;
using static Azure.Core.HttpHeader;
using Rasta.ProjectManagment.Application.Common.LocalizationManager;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IResourceManager, ResourceManager>();
        services.AddHttpContextAccessor();

        services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

        services.AddControllersWithViews();

        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddCors(options =>
        {
            options.AddPolicy(name: "RastaCorsPolicy",
                              policy =>
                              {
                                  policy.AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .AllowCredentials()
                                 .WithOrigins(AppSettings.Build().CORSTrustedOrigins);
                              });
        });

        services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = AppSettings.Build().IdentityServerAddress;
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.Audience = "app.projectManagment.api";
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                });

        return services;
    }
}
