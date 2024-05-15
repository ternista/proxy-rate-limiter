# Proxy API for Metadata Ingestion Service

This repository contains a proxy API for a metadata ingestion service. The API facilitates the retrieval of Star Wars universe metadata from the SWAPI database.

## Endpoints
- **GET /people/{id}**: Returns information about a single person.
- **GET /planets?pageIndex={x}&pageSize={y}**: Returns a paginated list of planets with aggregated residents information.

## Run API project
To run the API project, execute the following command:

```
dotnet run --project Api/Api.csproj
```
Afterward, navigate to http://localhost:5218/swagger/index.html in your browser to access the API explorer.

## Testing

Run the following command to execute all tests in the solution:
```
dotnet test .
```
The solution includes two types of tests:
    - unit tests for the rate limiting algorithm. 
    - integration tests for `/people` endpoint. These tests use WireMock for stubbing external API dependencies.

## Solution Structure

- Api/: Web API project containing API controllers, middleware, view models, and mapping profiles to map Application Model to Presentation View Models.
- Api.IntegrationTests/: Integration tests for the API.
- Application/: Application-specific services, models, and mapping profiles.
- Core/: Core types and utility methods.
- Infrastructure/: Code related to data access and communication with external services.
- Infrastructure.UnitTests/: Unit tests for some infrastructure components.

## Rate limiting implementation

The rate limiting functionality in this service is implemented using the token bucket algorithm, providing the ability to accommodate request bursts while maintaining a controlled rate of requests over time.

### Token Bucket Algorithm 
This algorithm allows for request bursts by replenishing tokens at a fixed rate. Requests are served if tokens are available and excess requests are rejected.

### Scalability
The current implementation maintains rate limiting state in-memory. For real-world scenarios, moving this state to a distributed cache like Redis is recommended. However, this transition needs careful considerations for a possible increase in latency, eventual state consistency, concurrency, and possible network issues. Additionally, a fallback strategy should be implemented to handle situations where connection to the distributed cache is unavailable.

### Where to limit
The rate limiting logic is currently implemented within the HTTP request handler (`RateLimitingRequestHandler`) for simplicity. A more advanced approach can involve moving this logic to data providers. This allows for more efficient rate limiting by considering the total number of requests required to retrieve the aggregated data, rather than limiting each individual request.

## Possible additional improvements
- Response Caching: Introduce response caching to reduce the number of requests made to the external API, improving overall performance and reducing the load.
- Better Error Handling: Implement robust error handling for scenarios such as SWAPI unavailability or transient issues. We can consider using circut breaker and falling back to last cached response if SWAPI is not reacheable.
- Throttling Handling: Implement strategies such as exponential backoff to complement API rate limiting.