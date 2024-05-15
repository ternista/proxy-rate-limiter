using System.Text.Json.Serialization;

namespace Infrastructure.StarwarsApi;

/// <summary>
/// Planet resource.
/// </summary>
public record Planet : BaseResource
{
	/// <summary>
    /// Ghe name of the planet.
    /// </summary>
	[JsonPropertyName("name")]
	public string Name { get; init; } = string.Empty;

	/// <summary>
    /// Diameter of the planet in kilometers.
    /// Can return "unknown" as the value.
    /// </summary>
	[JsonPropertyName("diameter")]
	public string Diameter { get; set; } = string.Empty;

	/// <summary>
    /// Number of standard hours it takes for the planet to complete
	/// a single rotation on its axis. Can return "unknown" as the value.
    /// </summary>
	[JsonPropertyName("rotation_period")]
	public string RotationPeriod { get; set; } = string.Empty;

	/// <summary>
    /// The number of standard days it takes for the planet to complete
	/// a single orbit of its local star.
    /// Can return "unknown" as the value.
    /// </summary>
	[JsonPropertyName("orbital_period")]
	public string OrbitalPeriod { get; set; } = string.Empty;

	/// <summary>
    /// A number denoting the gravity of the planet, where "1" is normal
	/// or 1 standard G. "2" is twice or 2 standard Gs. "0.5" is half or 0.5 standard Gs.
    /// Can return "unknown" as the value.
    /// </summary>
	[JsonPropertyName("gravity")]
	public string Gravity { get; set; } = string.Empty;

	/// <summary>
    /// The average population of sentient beings inhabiting this planet.
    /// Can return "unknown" as the value.
    /// .</summary>
	[JsonPropertyName("population")]
	public string Population { get; set; } = string.Empty;

	/// <summary>
    /// The climate of the planet.
    /// Comma separated if diverse.
    /// </summary>
	[JsonPropertyName("climate")]
	public string Climate { get; set; } = string.Empty;

	/// <summary>
    /// The terrain of the planet.
    /// Comma separated if diverse
    /// </summary>
	[JsonPropertyName("terrain")]
	public string Terrain { get; set; } = string.Empty;

	/// <summary>
    /// The percentage of the planet surface that is naturally occurring water or bodies of water.
    /// </summary>
	[JsonPropertyName("surface_water")]
	public string SurfaceWater { get; set; } = string.Empty;

	/// <summary>
    /// An array of People URL resources that live on the planet.
    /// </summary>
	[JsonPropertyName("residents")]
	public ICollection<Uri> Residents { get; set; } = [];

	/// <summary>
    /// An array of Film URL resources that the planet has appeared in.
    /// </summary>
	[JsonPropertyName("films")]
	public ICollection<Uri> Films { get; set; } = [];
}
