using Core;
using FluentAssertions;
using Infrastructure.Ratelimiting;
using Moq;

namespace Infrastructure.UnitTests;

public class TokenBucketRequestRateLimiterTests
{
    [Theory]
    [InlineData(10, 1)]
    [InlineData(11, 8)]
    [InlineData(22, 22)]
    public async void CanAquireLease_WhenRequstLimitIsNotReached_ReturnsTrue(int capacityPerWindow, int numberOfRequests)
    {
        // arrange
        var timeWindowInSeconds = 60;
        var dateTimeProvider = StaticDateTimeProvider(DateTime.UtcNow);

        var limiter = new TokenBucketRequestRateLimiter(dateTimeProvider.Object, new RateLimitingConfiguration(capacityPerWindow, capacityPerWindow, timeWindowInSeconds));

        // act & assert
        for (int i = 0; i < numberOfRequests; i++)
        {
            var result = await limiter.TryAcquireLease(CancellationToken.None);
            result.Should().BeTrue();
        }
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(10, 11)]
    public async void CanAquireLease_WhenRequstLimitIsReached_ReturnsFalse(int capacityPerWindow, int numberOfRequests)
    {
        // arrange
        var timeWindowInSeconds = 60; 
        var dateTimeProvider = StaticDateTimeProvider(DateTime.UtcNow);
        var limiter = new TokenBucketRequestRateLimiter(dateTimeProvider.Object, new RateLimitingConfiguration(capacityPerWindow, capacityPerWindow, timeWindowInSeconds));

        for (int i = 0; i < numberOfRequests -1; i++) 
        {
            await limiter.TryAcquireLease(CancellationToken.None);
        }

        // act
        var result = await limiter.TryAcquireLease(CancellationToken.None);

        // assert
        result.Should().BeFalse();
    }

    [Fact]
    public async void CanAquireLease_WhenTokensAreRefilled_ReturnsTrue()
    {
        // arrange
        var timeWindowInSeconds = 60; 
        var capacityPerWindow = 10;

        var seedTime = DateTime.UtcNow;
        var dateTimeProvider = StaticDateTimeProvider(seedTime);
        var limiter = new TokenBucketRequestRateLimiter(dateTimeProvider.Object, new RateLimitingConfiguration(capacityPerWindow, capacityPerWindow, timeWindowInSeconds));

        for (int i = 0; i < capacityPerWindow + 1; i++) 
        {
            await limiter.TryAcquireLease(CancellationToken.None);
        }

        dateTimeProvider.Setup(x => x.Now())
            .Returns(seedTime.AddSeconds(timeWindowInSeconds + 1));
        
        // act
        var result = await limiter.TryAcquireLease(CancellationToken.None);
        
        // assert
        result.Should().BeTrue();
    }

    [Fact]
    public async void CanAquireLease_WithUnusedBurstCapacity_CanExceedLimit()
    {
        // arrange
        var timeWindowInSeconds = 60; 
        var capacityPerWindow = 10;
        var maxBurstCapacity = 100;
        
        var seedTime = DateTime.UtcNow;
        var calls = 0;
        var dateTimeProvider = new Mock<IDateTimeProvider>();
        dateTimeProvider.Setup(x => x.Now())
            .Returns(() => seedTime.AddSeconds(timeWindowInSeconds * calls++));
        var limiter = new TokenBucketRequestRateLimiter(dateTimeProvider.Object, new RateLimitingConfiguration(capacityPerWindow, maxBurstCapacity, timeWindowInSeconds));

        // act & assert
        for (int i = 0; i < capacityPerWindow * 2; i++) 
        {
            var result = await limiter.TryAcquireLease(CancellationToken.None);
            result.Should().BeTrue();
        }
    }

    /// <summary>
    /// Date time provider mock that returns the same timestamp every call.
    /// </summary>
    private static Mock<IDateTimeProvider> StaticDateTimeProvider(DateTime seedTime)
    {
        var dateTimeProvider = new Mock<IDateTimeProvider>();
       
        dateTimeProvider.Setup(x => x.Now())
            .Returns(() => seedTime);
        
        return dateTimeProvider;
    }
}