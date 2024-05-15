using Api.ViewModels;
using Application.Contract;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PeopleController : ControllerBase
{
    private IPeopleDataProvider _peopleDataProvider;

    private readonly IMapper _mapper;

    public PeopleController(IPeopleDataProvider peopleDataProvider, IMapper mapper)
    {
        _peopleDataProvider = peopleDataProvider;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]   
    public async Task<ActionResult<PersonViewModel>> GetPerson(string id, CancellationToken cancellationToken)
    {
        var person = await _peopleDataProvider.GetPerson(id, cancellationToken);
        if (person == null) 
        {
            return NotFound();
        }

        return _mapper.Map<PersonViewModel>(person);
    }
}
