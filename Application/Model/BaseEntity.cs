namespace Application.Model;

/// <summary>
/// Base class for application entity.
/// </summary>
public abstract record BaseEntity
{
    /// <summary>
    /// Id of the resource.
    /// </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Time when this entity was created.
    /// </summary>
    public DateTimeOffset Created { get; init; }

    /// <summary>
    /// Time when this entity was last edited.
    /// </summary>
    public DateTimeOffset Edited { get; init; }
}