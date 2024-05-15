namespace Infrastructure.Ratelimiting;

/// <summary>
/// Bucket that holds currently available tokens. 
/// </summary>
public sealed class TokenBucket
{
    /// <summary>
    /// Total number of available tokens.
    /// </summary>
    public double AvailableTokens {get; private set;}
    
    /// <summary>
    /// Last time bucket was refilled.
    /// </summary>
    public long LastRefillTimestampInSeconds {get; private set;}

    public TokenBucket(double availableTokens, long initTimestampInSeconds)
    {
        AvailableTokens = availableTokens;
        LastRefillTimestampInSeconds = initTimestampInSeconds;
    }

    /// <summary>
    /// Refills bucket with new tokens.
    /// </summary>
    /// <param name="newAvailableTokens">New number of available tokens.</param>
    /// <param name="updateTimeStampInSeconds">Refill timestamp</param>
    public void RefillTokens(double newAvailableTokens, long updateTimeStampInSeconds) 
    {
        if (newAvailableTokens < 0) 
        {
            throw new ArgumentOutOfRangeException(nameof(newAvailableTokens));
        }

        if (updateTimeStampInSeconds < 0) 
        {
            throw new ArgumentOutOfRangeException(nameof(updateTimeStampInSeconds));
        }

        AvailableTokens = newAvailableTokens;
        LastRefillTimestampInSeconds = updateTimeStampInSeconds;
    }

    /// <summary>
    /// Consume one of the tokens if available.
    /// </summary>
    public void ConsumeToken() 
    {
        if (!HasAvailableTokens())
        {
            throw new InvalidOperationException("Bucket doesn't have enough available tokens.");
        } 

        AvailableTokens--;
    }

    /// <summary>
    /// Returns true if bucket has at least one token available.
    /// </summary>
    public bool HasAvailableTokens()
    {
        return AvailableTokens >= 1;
    }
}