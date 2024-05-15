using System.Text.Json.Serialization;

namespace Infrastructure.StarwarsApi;

/// <summary>
/// Person resource.
/// </summary>
public record Person : BaseResource
{
    /// <summary>
    /// Name of the persion.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// The birth year of the person, using the in-universe standard of BBY or ABY - Before the Battle of Yavin or After the Battle of Yavin. The Battle of Yavin is a battle that occurs at the end of Star Wars episode IV: A New Hope.
    /// Can be "unknown".
    /// </summary>
    [JsonPropertyName("birth_year")]
    public string BirthYear { get; init; } = string.Empty;

    /// <summary>
    /// The eye color of this person. Will be "unknown" if not known or "n/a" if the person does not have an eye.
    /// </summary>
    [JsonPropertyName("eye_color")]
    public string EyeColor { get; init; } = string.Empty;

    /// <summary>
    /// The gender of this person. Either "Male", "Female" or "unknown", "n/a" if the person does not have a gender.
    /// </summary>
    [JsonPropertyName("gender")]
    public string Gender { get; init; } = string.Empty;

    /// <summary>
    /// The hair color of this person. Will be "unknown" if not known or "n/a" if the person does not have hair.
    /// </summary>
    [JsonPropertyName("hair_color")]
    public string HairColor { get; init; } = string.Empty;

    /// <summary>
    /// The height of the person in centimeters.
    /// Can be "unknown".
    /// </summary>
    [JsonPropertyName("height")]
    public string Height { get; init; } = string.Empty;

    /// <summary>
    /// The mass of the person in kilograms.
    /// Can be "unknown".
    /// </summary>
    [JsonPropertyName("mass")]
    public string Mass { get; init; } = string.Empty;

    /// <summary>
    /// The skin color of this person.
    /// Can be "unknown".
    /// </summary>
    [JsonPropertyName("skin_color")]
    public string SkinColor { get; init; } = string.Empty;

    /// <summary>
    /// The URL of a planet resource, a planet that this person was born on or inhabits.
    /// </summary>
    [JsonPropertyName("homeworld")]
    public Uri Homeworld { get; init; } = null!;

    /// <summary>
    /// An array of film resource URLs that this person has been in.
    /// </summary>
    [JsonPropertyName("films")]
    public ICollection<Uri> Films { get; init; } = [];

    /// <summary>
    /// An array of species resource URLs that this person belongs to.
    /// </summary>
    [JsonPropertyName("species")]
    public ICollection<Uri> Species { get; init; } = [];

    /// <summary>
    ///  An array of starship resource URLs that this person has piloted.
    /// </summary>
    [JsonPropertyName("starships")]
    public ICollection<Uri> Starships { get; init; } = [];

    /// <summary>
    /// An array of vehicle resource URLs that this person has piloted.
    /// </summary>
    [JsonPropertyName("vehicles")]
    public ICollection<Uri> Vehicles { get; init; } = [];
}
