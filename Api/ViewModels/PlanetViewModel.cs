
namespace Api.ViewModels;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// A Planet resource is a large mass, planet or planetoid in the Star Wars Universe, at the time of 0 ABY.
/// </summary>
public record PlanetViewModel : BaseResourceViewModel
{
    /// <summary>
    /// The name of this planet if known.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// The diameter of this planet in kilometers, if known.
    /// </summary>
    public int? DiameterInMeters { get; init; }

    /// <summary>
    /// The number of standard hours it takes for this planet to complete a single rotation on its axis, if known.
    /// </summary>
    public int? RotationPeriodInHours { get; init; }

    /// <summary>
    /// The number of standard days it takes for this planet to complete a single orbit of its local star, if known.
    /// </summary>
    [Required]
    public int? OrbitalPeriodInDays { get; init; }

    /// <summary>
    /// A number denoting the gravity of this planet, where "1" is normal or 1 standard G. "2" is twice or 2 standard Gs. "0.5" is half or 0.5 standard Gs.
    /// </summary>
    public string? Gravity { get; init; }

    /// <summary>
    /// The average population of sentient beings inhabiting this planet.
    /// </summary>
    public int? Population { get; init; }

    /// <summary>
    /// The climate of this planet. Comma separated if diverse.
    /// </summary>
    public string? Climate { get; init; }

    /// <summary>
    /// The terrain of this planet. Comma separated if diverse.
    /// </summary>
    public string? Terrain { get; init; }

    /// <summary>
    /// The percentage of the planet surface that is naturally occurring water or bodies of water, if known.
    /// </summary>
    public double? SurfaceWaterPercentage { get; init; }

    /// <summary>
    /// An array of residents that live on this planet.
    /// </summary>
    [Required]
    public ICollection<ResidentViewModel> Residents { get; init; } = new List<ResidentViewModel>();
}
