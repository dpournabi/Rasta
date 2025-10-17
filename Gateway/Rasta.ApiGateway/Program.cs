using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Rasta.ApiGateway;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "RastaCorsPolicy",
                      policy =>
                      {
                          policy.AllowAnyHeader()
                         .AllowAnyMethod()
                         .AllowCredentials()
                         .WithOrigins(AppSettings.Build().AllowOrigins);
                      });
});
var app = builder.Build();

app.MapControllers();
app.UseOcelot();
app.UseSwagger();
app.UseSwaggerUI(option =>
{
    option.SwaggerEndpoint("/swagger/v1/swagger.json", "Rasta.ApiGateway v1");
    option.RoutePrefix = string.Empty;
});
app.Run();
