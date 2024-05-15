namespace Application.Contract;

using Application.Model;
using Core;

public interface IPlanetDataProvider
{
    /// <summary>
    /// Returns paginated list of planets and their residernts.
    /// </summary>
    Task<CollectionResult<PlanetWithResidents>> GetPlanetsWithResidents(PaginationOptions paginationOptions, CancellationToken cancellationToken);
}
