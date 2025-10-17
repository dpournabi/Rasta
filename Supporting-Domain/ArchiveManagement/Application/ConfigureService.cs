using Application.Implementations;
using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ConfigureService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IMinioClient, MinioClient>();
            services.AddScoped<IMinIoHelperService, MinIoHelperService>();
            services.AddScoped<IArchiveCategoryService, ArchiveCategoryService>();
            services.AddScoped<IArchiveItemService, ArchiveItemService>();

            return services;
        }
    }
}
