using Infrastructure.Ratelimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DI;

public static class RateLimitingRegistrationExtensions 
{
    public static IServiceCollection RegisterRateLimiting(
        this IServiceCollection services,
        IConfiguration configuration) 
    {
        var rateLimitingConfiguration = configuration.GetRequiredSection(nameof(RateLimitingConfiguration)).Get<RateLimitingConfiguration>();
        if (rateLimitingConfiguration == null) 
        {
            throw new ArgumentException("Can't find RateLimitingConfiguration");
        }
        
        services.AddSingleton(rateLimitingConfiguration);
        services.AddSingleton<IRequestRateLimiter, TokenBucketRequestRateLimiter>();

        return services;
    }
}