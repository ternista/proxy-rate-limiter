namespace Api.ViewModels;

/// <summary>
/// An individual person or character within the Star Wars universe
/// </summary>
public record ResidentViewModel : BaseResourceViewModel
{
    /// <summary>
    /// The name of this person, if known.
    /// </summary>
    public string? Name { get; init; }
}
