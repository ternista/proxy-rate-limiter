using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace Api.IntegrationTests;

public class ApiWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly StarwarsApiFixture _starwarsApiFixture;

    public StarwarsApiFixture StarwarsApi => _starwarsApiFixture;

    public ApiWebApplicationFactory()
    {
        _starwarsApiFixture = new StarwarsApiFixture();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
                        {
                            ["StarwarsApiConfiguration:BaseUri"] = _starwarsApiFixture.GetUrl()
                        })
            .Build(); 

        builder.UseConfiguration(configuration);
    }
}