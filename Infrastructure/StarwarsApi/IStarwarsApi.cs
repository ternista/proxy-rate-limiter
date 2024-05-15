using Refit;

namespace Infrastructure.StarwarsApi;

public interface IStarwarsApi
{
    [Get("/planets")]
    Task<ResourceCollection<Planet>> GetPlantes(int? page = null, int? size = null, CancellationToken cancellationToken = default);

    [Get("/people/{id}")]
    Task<Person> GetPerson(string id, CancellationToken cancellationToken = default);
}
