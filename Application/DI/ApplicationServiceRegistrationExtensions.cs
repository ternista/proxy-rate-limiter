using Application.Contract;
using Application.Service;
using Core;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DI;

public static class ApplicationServiceRegistrationExtensions 
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services) 
    {
        services.AddTransient<IPeopleDataProvider, PeopleDataProvider>();
        services.AddTransient<IPlanetDataProvider, PlanetDataProvider>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}