namespace Api.ViewModels;

/// <summary>
/// An individual person or character within the Star Wars universe
/// </summary>
public record PersonViewModel : BaseResourceViewModel
{
    /// <summary>
    /// The name of this person, if known.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// The birth year of the person, using the in-universe standard of BBY or ABY - Before the Battle of Yavin or After the Battle of Yavin. The Battle of Yavin is a battle that occurs at the end of Star Wars episode IV: A New Hope, if known.
    /// </summary>
    public string? BirthYear { get; init; }

    /// <summary>
    /// The eye color of this person, if known.
    /// </summary>
    public string? EyeColor { get; init; }

    /// <summary>
    /// The gender of this person, if known.
    /// </summary>
    public string? Gender { get; init; }

    /// <summary>
    /// The hair color of this person, if known.
    /// </summary>
    public string? HairColor { get; init; }

    /// <summary>
    /// The height of the person in centimeters.
    /// </summary>
    public int? HeightInCentimeters { get; init; }

    /// <summary>
    /// The mass of the person in kilograms.
    /// </summary>
    public int? MassInKilograms { get; init; }

    /// <summary>
    /// The skin color of this person, if known.
    /// </summary>
    public string? SkinColor { get; init; }
}
