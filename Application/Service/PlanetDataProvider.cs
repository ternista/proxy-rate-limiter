using Application.Contract;
using Application.Mapping;
using Application.Model;
using AutoMapper;
using Core;
using Infrastructure.StarwarsApi;

namespace Application.Service;

public class PlanetDataProvider : IPlanetDataProvider
{
    private readonly IStarwarsApi _starwarsApi;
    private readonly IPeopleDataProvider _peopleDataProvider;
    private readonly IMapper _mapper;

    public PlanetDataProvider(IStarwarsApi starwarsApi, 
    IPeopleDataProvider peopleDataProvider,
    IMapper mapper)
    {
        _starwarsApi = starwarsApi;
        _peopleDataProvider = peopleDataProvider;
        _mapper = mapper;
    }

    public async Task<CollectionResult<PlanetWithResidents>> GetPlanetsWithResidents(PaginationOptions paginationOptions, CancellationToken cancellationToken)
    {
        var planets = await _starwarsApi.GetPlantes(paginationOptions.PageIndex, paginationOptions.PageSize);
        var peopleIds = planets.Results.SelectMany(x => x.Residents)
            .Distinct()
            .Select(x => x.GetResourceIdFromUri())
            .ToHashSet();

        var people = await _peopleDataProvider.GetPeople(peopleIds, cancellationToken);
        var peopleMap = people.ToDictionary(x => x.Id);

        return new CollectionResult<PlanetWithResidents>(
            planets.Results.Select(planet => MergePlanetWithResidents(planet, peopleMap)).ToArray(),
            planets.Count);
    }

    private PlanetWithResidents MergePlanetWithResidents(Planet planet, Dictionary<string, Model.Person> peopleMap)
    {
        var residents = planet
            .Residents
            .Select(residentUri => residentUri.GetResourceIdFromUri())
            .Select(residentId => peopleMap.ContainsKey(residentId) ? peopleMap[residentId] : null)
            .Where(resident => resident != null)
            .ToArray();

        var planetWithResidents = _mapper.Map<PlanetWithResidents>(planet);
        planetWithResidents.Residents = residents!;
        
        return planetWithResidents;
    }
}