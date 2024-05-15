namespace Infrastructure.Ratelimiting;

/// <summary>
/// Represents a request limiter that determines if operation can proceed based on predefined conditions.
/// </summary>
public interface IRequestRateLimiter 
{
    /// <summary>
    /// Attempt to aquire lease for a single request.
    /// </summary>
    /// <returns>True when attempt was successfull, false otherwise.</returns>
    Task<bool> TryAcquireLease(CancellationToken cancellationToken);
}
