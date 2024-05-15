using AutoMapper;
using Core;

namespace Application.Mapping;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
	{
        CreateMap(typeof(CollectionResult<>), typeof(CollectionResult<>));

        CreateMap<Infrastructure.StarwarsApi.BaseResource, Model.BaseEntity>()
            .ForMember(x => x.Id, opts => opts.MapFrom(x => x.Url.GetResourceIdFromUri()));

        CreateMap<Infrastructure.StarwarsApi.Planet, Model.PlanetWithResidents>()
            .IncludeBase<Infrastructure.StarwarsApi.BaseResource, Model.BaseEntity>()
            .ForMember(x => x.Name, opts => opts.MapFrom(x => GetStringValueOrDefault(x.Name)))
            .ForMember(x => x.DiameterInMeters, opts => opts.MapFrom(x => GetIntValueOrDefault(x.Diameter)))
            .ForMember(x => x.OrbitalPeriodInDays, opts => opts.MapFrom(x => GetIntValueOrDefault(x.OrbitalPeriod)))
            .ForMember(x => x.RotationPeriodInHours, opts => opts.MapFrom(x => GetIntValueOrDefault(x.RotationPeriod)))
            .ForMember(x => x.Population, opts => opts.MapFrom(x => GetIntValueOrDefault(x.Population)))
            .ForMember(x => x.Climate, opts => opts.MapFrom(x => GetStringValueOrDefault(x.Climate)))
            .ForMember(x => x.Terrain, opts => opts.MapFrom(x => GetStringValueOrDefault(x.Terrain)))
            .ForMember(x => x.SurfaceWaterPercentage, opts => opts.MapFrom(x => GetDoubleValueOrDefault(x.SurfaceWater)))
            .ForMember(x => x.Residents, opts => opts.Ignore());

        CreateMap<Infrastructure.StarwarsApi.Person, Model.Person>()
            .IncludeBase<Infrastructure.StarwarsApi.BaseResource, Model.BaseEntity>()
            .ForMember(x => x.Name, opts => opts.MapFrom(x => GetStringValueOrDefault(x.Name)))
            .ForMember(x => x.BirthYear, opts => opts.MapFrom(x => GetStringValueOrDefault(x.BirthYear)))
            .ForMember(x => x.EyeColor, opts => opts.MapFrom(x => GetStringValueOrDefault(x.EyeColor)))
            .ForMember(x => x.Gender, opts => opts.MapFrom(x => GetStringValueOrDefault(x.Gender)))
            .ForMember(x => x.HairColor, opts => opts.MapFrom(x => GetStringValueOrDefault(x.HairColor)))
            .ForMember(x => x.HeightInCentimeters, opts => opts.MapFrom(x => GetIntValueOrDefault(x.Height)))
            .ForMember(x => x.MassInKilograms, opts => opts.MapFrom(x => GetDoubleValueOrDefault(x.Mass)))
            .ForMember(x => x.SkinColor, opts => opts.MapFrom(x => GetStringValueOrDefault(x.SkinColor)));

	}

      public static string? GetStringValueOrDefault(string value) 
    {
        if (string.IsNullOrWhiteSpace(value)) 
        {
            return null;
        }

        if (value == "unknown" || value  == "n/a") 
        {
            return null;
        }

        return value;
    }

    public static int? GetIntValueOrDefault(string value) 
    {
        if (string.IsNullOrWhiteSpace(value)) 
        {
            return null;
        }

        if (int.TryParse(value, out var valueValue)) 
        {
            return valueValue;
        }

        return null;
    }

    public static double? GetDoubleValueOrDefault(string value) 
    {
        if (string.IsNullOrWhiteSpace(value)) 
        {
            return null;
        }

        if (double.TryParse(value, out var valueValue)) 
        {
            return valueValue;
        }

        return null;
    }
}
