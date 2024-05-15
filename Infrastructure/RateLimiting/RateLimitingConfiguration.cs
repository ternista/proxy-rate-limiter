namespace Infrastructure.Ratelimiting;

/// <summary>
/// Configuration for request rate limiter.
/// </summary>
/// <param name="CapacityPerWindow">Determines how fast new tokens are refilled.</param>
/// <param name="MaxBurstCapacity">Maximum allowed burst capacity.</param>
/// <param name="WindowTimeInSeconds">Request reate measurement window.</param>
public record RateLimitingConfiguration(
    double CapacityPerWindow, 
    int MaxBurstCapacity, 
    double WindowTimeInSeconds);
