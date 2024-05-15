namespace Api.ViewModels;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Base class for API resource
/// </summary>
public abstract record BaseResourceViewModel
{
    /// <summary>
    /// Id of the resource.
    /// </summary>
    [Required]
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Time when this resource was created.
    /// </summary>
    [Required]
    public DateTimeOffset Created { get; init; }

    /// <summary>
    /// Time when this resource was last edited.
    /// </summary>
    [Required]
    public DateTimeOffset Edited { get; init; }
}