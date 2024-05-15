namespace Core;

/// <summary>
/// Exception thrown by application when number of requests to API endpoint reached allowed limits.
/// </summary>
public class RateLimitReachedException : Exception 
{
    public RateLimitReachedException(string message): base(message)
    {
    }
}