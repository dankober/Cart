namespace Cart.Api.Services;

public interface IHelloService
{
    string GetGreeting(string? name = null);
}
