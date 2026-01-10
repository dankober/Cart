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
    public void GetGreeting_WithoutName_ReturnsDefaultMessage()
    {
        // Act
        var result = _sut.GetGreeting();

        // Assert
        Assert.Equal("Hello from Cart API", result);
    }

    [Fact]
    public void GetGreeting_WithName_ReturnsPersonalizedMessage()
    {
        // Act
        var result = _sut.GetGreeting("Alice");

        // Assert
        Assert.Equal("Hello, Alice!", result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void GetGreeting_WithNullOrWhitespaceName_ReturnsDefaultMessage(string? name)
    {
        // Act
        var result = _sut.GetGreeting(name);

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
