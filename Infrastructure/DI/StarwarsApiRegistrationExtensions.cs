using Infrastructure.Http;
using Infrastructure.StarwarsApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Infrastructure.DI;

public static class StarwarsApiRegistrationExtensions 
{
    public static IServiceCollection RegisterStarwarsApi(
        this IServiceCollection services,
        IConfiguration configuration) 
    {
        var swapiConfiguration = configuration.GetRequiredSection(nameof(StarwarsApiConfiguration)).Get<StarwarsApiConfiguration>();
        if (swapiConfiguration == null) 
        {
            throw new ArgumentException("Can't find StarwarsApiConfiguration");
        }

        services.AddTransient<RateLimitingRequestHandler>();

        services
            .AddRefitClient<IStarwarsApi>()
            .ConfigureHttpClient(c => {
                c.BaseAddress = new Uri(swapiConfiguration.BaseUri);
            })
            .AddHttpMessageHandler<RateLimitingRequestHandler>();

        return services;
    }
}