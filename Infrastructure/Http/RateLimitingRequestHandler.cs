using Core;
using Infrastructure.Ratelimiting;

namespace Infrastructure.Http;

/// <summary>
/// Http client extensions that limits number of requests done to external API.
/// When there is no capacity left for new requests during current time window, it throws RateLimitReachedException exception.
/// </summary>
public class RateLimitingRequestHandler : DelegatingHandler 
{
    private readonly IRequestRateLimiter _requestRateLimiter;

    public RateLimitingRequestHandler(IRequestRateLimiter requestRateLimiter)
    {
        _requestRateLimiter = requestRateLimiter;
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (await _requestRateLimiter.TryAcquireLease(cancellationToken)) 
        {
            return await base.SendAsync(request, cancellationToken);
        }

        throw new RateLimitReachedException("Too many requests. Please try again later.");
    }
}