using System.Text.Json.Serialization;

namespace Infrastructure.StarwarsApi;

public abstract record BaseResource
{
    /// <summary>
    /// The URL of the resource.
    /// </summary>
    [JsonPropertyName("url")]
    public Uri Url { get; init; } = null!;

    /// <summary>
    /// Date of creation of the resource.
    /// </summary>
    [JsonPropertyName("created")]
    public DateTime Created { get; init; }

    /// <summary>
    /// Dte of last modification of the resource.
    /// </summary>
    [JsonPropertyName("edited")]
    public DateTime Edited { get; init; }
}