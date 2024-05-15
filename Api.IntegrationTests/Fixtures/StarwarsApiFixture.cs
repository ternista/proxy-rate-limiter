using System.Net;
using Infrastructure.StarwarsApi;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Api.IntegrationTests;

public class StarwarsApiFixture : IDisposable
{
    private readonly WireMockServer _mockServer;
    
    public StarwarsApiFixture()
    {
        _mockServer = WireMockServer.Start();
    }

    public string GetUrl() => _mockServer?.Url ?? throw new InvalidOperationException("Fixture was not initialized.");

    public void SetPlanetsResponse(ICollection<Planet> planets) 
    {
        _mockServer.Given(Request.Create()
            .UsingGet()
            .WithPath("/planets"))
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(new ResourceCollection<Planet>{ Count = planets.Count, Results = planets }));
    }

    public void SetPersonResponse(string id, Person person) 
    {
        _mockServer.Given(Request.Create()
            .UsingGet()
            .WithPath($"/people/{id}"))
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(person));
    }

    public void SetPersonResponse(string id, HttpStatusCode statusCode) 
    {
        _mockServer.Given(Request.Create()
            .UsingGet()
            .WithPath($"/people/{id}"))
            .RespondWith(Response.Create()
                .WithStatusCode(statusCode));
    }

    public void Dispose()
    {
        _mockServer.Reset();
        _mockServer.Stop();
    }
}