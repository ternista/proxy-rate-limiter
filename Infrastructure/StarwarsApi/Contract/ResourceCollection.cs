using System.Text.Json.Serialization;

namespace Infrastructure.StarwarsApi;

public record ResourceCollection<T> where T : BaseResource
{
    /// <summary>
    /// Paged results.
    /// </summary>
    [JsonPropertyName("results")]
    public ICollection<T> Results { get; init; } = [];

    /// <summary>
    /// Total Count of the results.
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; init; }

    /// <summary>
    /// Next page url
    /// </summary>
    [JsonPropertyName("next")]
    public string Next { get; init; } = string.Empty;

    /// <summary>
    /// Previous page url.
    /// </summary>
    [JsonPropertyName("previous")]
    public string Previous { get; init; } = string.Empty;
}