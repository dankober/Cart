namespace Cart.Api.Services;

public class HelloService : IHelloService
{
    public string GetGreeting(string? name = null)
    {
        return string.IsNullOrWhiteSpace(name)
            ? "Hello from Cart API"
            : $"Hello, {name}!";
    }
}
