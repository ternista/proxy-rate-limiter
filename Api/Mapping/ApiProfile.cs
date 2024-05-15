using Api.ViewModels;
using Application.Model;
using AutoMapper;

namespace Api.Mapping;

public class ApiProfile : Profile
{
    public ApiProfile()
	{
        CreateMap<PlanetWithResidents, PlanetViewModel>();

        CreateMap<Person, ResidentViewModel>();
        CreateMap<Person, PersonViewModel>();
	}
}