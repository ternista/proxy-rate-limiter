using Application.Contract;
using AutoMapper;
using Infrastructure.StarwarsApi;

namespace Application.Service;

public class PeopleDataProvider : IPeopleDataProvider
{
    private const int ReqestBatchSize = 100;
    private readonly IStarwarsApi _starwarsApi;
    private readonly IMapper _mapper;

    public PeopleDataProvider(IStarwarsApi starwarsApi, IMapper mapper)
    {
        _starwarsApi = starwarsApi;
        _mapper = mapper;
    }

    public async Task<ICollection<Application.Model.Person>> GetPeople(IEnumerable<string> ids, CancellationToken cancellationToken)
    {
        var results = new List<Application.Model.Person>();
        foreach (var chunks in ids.Chunk(ReqestBatchSize)) 
        {
            var requestsBatch = chunks.Select(id => GetPerson(id, cancellationToken));
            var residents = await Task.WhenAll(requestsBatch);
            results.AddRange(residents.Where(x => x is not null)!);
        }   

        return results;
    }

    public async Task<Model.Person> GetPerson(string id, CancellationToken cancellationToken)
    {   
        var person = await _starwarsApi.GetPerson(id, cancellationToken);
        return _mapper.Map<Application.Model.Person>(person);
    }
}