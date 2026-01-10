using Cart.Api.Services;

namespace Cart.Api.Tests.Services;

public class HelloServiceTests
{
    private readonly HelloService _sut;

    public HelloServiceTests()
    {
        _sut = new HelloService();
    }

    [Fact]
    public void GetGreeting_ReturnsExpectedMessage()
    {
        // Act
        var result = _sut.GetGreeting();

        // Assert
        Assert.Equal("Hello from Cart API", result);
    }

    [Fact]
    public void GetGreeting_ReturnsNonEmptyString()
    {
        // Act
        var result = _sut.GetGreeting();

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(result));
    }
}
