
using Application;
using ArchimeManagement.Models;
using Infrastructure;
using Minio;

namespace ArchimeManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var endpoint = "127.0.0.1";
            int port = 9000;
            var accessKey = "NquzH3gYGmeAV6xJMBSX";
            var secretKey = "xuc51YYe6NX8RR6rx0OaTDZ4vO55zTMvO5JwJwUD";

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMinio(configureClient =>
            {
                configureClient.WithSSL(false);
                configureClient.WithEndpoint(endpoint, port);
                configureClient.WithCredentials(accessKey, secretKey);
                //.WithRegion("us-east-1")
                configureClient.Build();
            });

            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices(builder.Configuration);

            builder.Services.AddCors(options =>
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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseCors();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
