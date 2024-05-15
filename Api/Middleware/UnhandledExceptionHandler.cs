using System.Net;
using Core;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api;

/// <summary>
/// Handles unhandled application exceptions and converts them to correct API response. 
/// </summary>
public class UnhandledExceptionHandler : IExceptionHandler
{
    private readonly ILogger<UnhandledExceptionHandler> _logger;
    
    public UnhandledExceptionHandler(ILogger<UnhandledExceptionHandler> logger)
    {
        _logger = logger;
    }
    
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);
        
        var problemDetails = CreateProblemDetailsFromException(exception);

        httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private static ProblemDetails CreateProblemDetailsFromException(Exception exception)
    {
        switch (exception) 
        {
            case Refit.ApiException refitException when refitException.StatusCode == HttpStatusCode.NotFound:
                return new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Resource with requested id is not found."
                };
            case Refit.ApiException refitException when refitException.StatusCode == HttpStatusCode.TooManyRequests:
            case RateLimitReachedException rateLimitReached:
                return new ProblemDetails
                {
                    Status = StatusCodes.Status429TooManyRequests,
                    Title = "Too many requests. Please retry later."
                };
            default:
                return new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Unexpected error, please try again."
                };
        }
    }
}