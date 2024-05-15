using Core;

namespace Infrastructure.Ratelimiting;

/// <summary>
/// Rate limiter implementing token-bucket algorithm - https://en.wikipedia.org/wiki/Token_bucket
/// A token bucket provides a mechanism that allows a desired level of burstiness within a flow by limiting its average rate as well as its maximum burst size. 
/// If client doesn't use entire capacity during the measurment window, we accumulate burst tokens as credits which are allowed to be consumed during request spikes. This will never exceed MaxBurstCapacity. 
/// </summary>
/// <notes>
/// This implementation stores its state in-memory. 
/// In real-world distributed deployment scenario, we should consider storing this state in a distributed cache.
/// </notes>
public class TokenBucketRequestRateLimiter : IRequestRateLimiter
{
    private static readonly object _lock = new();
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly double _capacityPerWindow;
    private readonly double _refillRatePerSecond;
    private readonly int _maxBurstCapacity;
    private readonly TokenBucket _tokenBucket;

    /// <summary>
    /// Creates new instance of request rate limiter
    /// </summary>
    public TokenBucketRequestRateLimiter(IDateTimeProvider dateTimeProvider, RateLimitingConfiguration configuration)
    {
        _dateTimeProvider = dateTimeProvider;
        _capacityPerWindow = configuration.CapacityPerWindow;
        _maxBurstCapacity = configuration.MaxBurstCapacity;
        _refillRatePerSecond = _capacityPerWindow/configuration.WindowTimeInSeconds;
        _tokenBucket = new TokenBucket(_capacityPerWindow, GetCurrentTimestampInSeconds());
    }

    /// <summary>
    /// Attempt to aquire lease for icoming request.
    /// </summary>
    /// <returns>True when attempt was successfull, false otherwise.</returns>
    public Task<bool> TryAcquireLease(CancellationToken cancellationToken)
    {
        lock (_lock)
        {
            TryRefillTokens();

            if (_tokenBucket.HasAvailableTokens())
            {
                _tokenBucket.ConsumeToken();
                return Task.FromResult(true);
            }
            else
            {
                // block request if bucket is empty
                return Task.FromResult(false);;
            }
        }
    }

    private long GetCurrentTimestampInSeconds()
    {
        return _dateTimeProvider.Now().ToUnixTimeSeconds();
    }

    /// <summary>
    /// Refills token bucket proportionally to elapsed time from the last refil.
    /// Tokens are accumulated at a steady rate until max burst capacity is reached. 
    /// </summary>
    private void TryRefillTokens()
    {
        long now = GetCurrentTimestampInSeconds();
        if (now > _tokenBucket.LastRefillTimestampInSeconds) 
        {
            var elapsedTimeInSecondsFromLastRefil = now - _tokenBucket.LastRefillTimestampInSeconds;
            var tokensToBeAdded = elapsedTimeInSecondsFromLastRefil * _refillRatePerSecond;
            var newAvailableTokens = Math.Min(_maxBurstCapacity, _tokenBucket.AvailableTokens + tokensToBeAdded);
            _tokenBucket.RefillTokens(newAvailableTokens, now);
        }
    }  
}
