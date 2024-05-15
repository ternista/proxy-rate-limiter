using Api.ViewModels;
using Application.Contract;
using AutoMapper;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PlanetsController : ControllerBase
{
    private IPlanetDataProvider _planetDataProvider;
    private readonly IMapper _mapper;

    public PlanetsController(IPlanetDataProvider planetDataProvider, IMapper mapper)
    {
        _planetDataProvider = planetDataProvider;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]   
    public async Task<CollectionResult<PlanetViewModel>> GetPlanets([FromQuery]PaginationOptions paginationOptions, CancellationToken cancellationToken)
    {
        var planets = await _planetDataProvider.GetPlanetsWithResidents(paginationOptions, cancellationToken);

        return _mapper.Map<CollectionResult<PlanetViewModel>>(planets);
    }
}
