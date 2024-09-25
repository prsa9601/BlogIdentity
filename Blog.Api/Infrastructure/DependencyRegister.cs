using AngleSharp;
using AspNetCoreRateLimit;
using Blog.Api.Infrastructure.Gateways.Zibal;
using Blog.Api.Infrastructure.JwtUtil;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Blog.Api.Infrastructure;

public static class DependencyRegister
{
    public static void RegisterApiDependency(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddAutoMapper(typeof(MapperProfile).Assembly);
        service.AddTransient<CustomJwtValidation>();
        service.AddHttpClient<IZibalService, ZibalService>();
        //ShopApi
        service.AddCors(options =>
        {
            options.AddPolicy(name: "App",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
        service.AddMemoryCache();

        //load general configuration from appsettings.json
        service.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));

        //load ip rules from appsettings.json
        service.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));

        // inject counter and rules stores
        service.AddInMemoryRateLimiting();

        service.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    }
}