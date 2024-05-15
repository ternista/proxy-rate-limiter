using System.Net.Http.Json;
using Api.ViewModels;
using Core;
using FluentAssertions;
using Infrastructure.StarwarsApi;

namespace Api.IntegrationTests;

public class PlanetsApiTests : IClassFixture<ApiWebApplicationFactory>
{
    private readonly ApiWebApplicationFactory _factory;

    public PlanetsApiTests(ApiWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_ReturnsSuccess_AndReturnsPlanetsWithResidents()
    {
        // arrange
        var client = _factory.CreateClient();
        SetupStarwarsApiResponse();

        // act
        var response = await client.GetAsync("/planets");

        // assert
        response.EnsureSuccessStatusCode();
        var planetsResult = await response.Content.ReadFromJsonAsync<CollectionResult<PlanetViewModel>>();
        planetsResult.Should().NotBeNull();
        planetsResult!.TotalCount.Should().Be(2);

        var planet1 = planetsResult.Results.Should().ContainSingle(x => x.Id == "1").Which;
        planet1.Name.Should().Be("Tatooine");
        planet1.Residents.Should().HaveCount(2);
        planet1.Residents.Should().Satisfy(x => x.Name == "Luke Skywalker" && x.Id == "1", 
            x => x.Name == "C-3PO" && x.Id == "2");

        var planet2 = planetsResult.Results.Should().ContainSingle(x => x.Id == "2").Which;
        planet2.Name.Should().Be("Alderaan");
        planet2.Residents.Should().HaveCount(1);
        planet2.Residents.Should().Satisfy(x => x.Name == "Leia Organa" && x.Id == "5");
    }

    [Fact]
    public async Task Get_ReturnsTooManyRequests_WhenSwapiApiRespondsWith429()
    {
        // arrange
        var client = _factory.CreateClient();
        SetupStarwarsApiResponse();
        _factory.StarwarsApi.SetPersonResponse("5", System.Net.HttpStatusCode.TooManyRequests);

        // act
        var response = await client.GetAsync("/planets");

        // assert
        response.Should().HaveStatusCode(System.Net.HttpStatusCode.TooManyRequests);
    }

    [Fact]
    public async Task Get_ReturnsNotFound_WhenSwapiApiRespondsWith404()
    {
        // arrange
        var client = _factory.CreateClient();
        SetupStarwarsApiResponse();
        _factory.StarwarsApi.SetPersonResponse("1", System.Net.HttpStatusCode.NotFound);

        // act
        var response = await client.GetAsync("/planets");

        // assert
        response.Should().HaveStatusCode(System.Net.HttpStatusCode.NotFound);
    }

    private void SetupStarwarsApiResponse()
    {
        _factory.StarwarsApi.SetPlanetsResponse([
            new Planet {
                Name = "Tatooine",
                RotationPeriod = "23",
                OrbitalPeriod = "304",
                Diameter = "10465",
                Climate = "arid",
                Gravity = "1 standard",
                Terrain = "desert",
                SurfaceWater = "1",
                Population = "200000",
                Residents = new Uri[] {
                    new Uri("https://swapi.dev/api/people/1/"),
                    new Uri("https://swapi.dev/api/people/2/")
                },
                Created = new DateTime(2021, 11, 12, 19, 11, 12),
                Edited = new DateTime(2024, 11, 12, 19, 11, 12),
                Url = new Uri("https://swapi.dev/api/planets/1/")
            },
            new Planet {
                Name = "Alderaan",
                RotationPeriod = "24",
                OrbitalPeriod = "364",
                Diameter = "12500",
                Climate = "temperate",
                Gravity = "1 standard",
                Terrain = "grasslands, mountains",
                SurfaceWater = "40",
                Population = "2000000000",
                Residents = new Uri[] {
                    new Uri("https://swapi.dev/api/people/5/")
                },
                Created = new DateTime(2021, 11, 12, 19, 11, 12),
                Edited = new DateTime(2024, 11, 12, 19, 11, 12),
                Url = new Uri("https://swapi.dev/api/planets/2/")
            }]);

        _factory.StarwarsApi.SetPersonResponse("1", new Person
        {
            Name = "Luke Skywalker",
            Height = "172",
            Mass = "77",
            HairColor = "blond",
            SkinColor = "fair",
            EyeColor = "blue",
            BirthYear = "19BBY",
            Gender = "male",
            Homeworld = new Uri("https://swapi.dev/api/planets/1/"),
            Created = new DateTime(2021, 11, 12, 19, 11, 12),
            Edited = new DateTime(2024, 11, 12, 19, 11, 12),
            Url = new Uri("https://swapi.dev/api/people/1/")
        });
        _factory.StarwarsApi.SetPersonResponse("2", new Person
        {
            Name = "C-3PO",
            Height = "167",
            Mass = "75",
            HairColor = "n/a",
            SkinColor = "gold",
            EyeColor = "yellow",
            BirthYear = "112BBY",
            Gender = "n/a",
            Homeworld = new Uri("https://swapi.dev/api/planets/1/"),
            Created = new DateTime(2021, 11, 12, 19, 11, 12),
            Edited = new DateTime(2024, 11, 12, 19, 11, 12),
            Url = new Uri("https://swapi.dev/api/people/2/")
        });
        _factory.StarwarsApi.SetPersonResponse("5", new Person
        {
            Name = "Leia Organa",
            Height = "150",
            Mass = "49",
            HairColor = "brown",
            SkinColor = "light",
            EyeColor = "brown",
            BirthYear = "19BBY",
            Gender = "female",
            Homeworld = new Uri("https://swapi.dev/api/planets/2/"),
            Created = new DateTime(2021, 11, 12, 19, 11, 12),
            Edited = new DateTime(2024, 11, 12, 19, 11, 12),
            Url = new Uri("https://swapi.dev/api/people/5/")
        });
    }
}