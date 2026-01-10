namespace Cart.Api.Services;

public class HelloService : IHelloService
{
    public string GetGreeting()
    {
        return "Hello from Cart API";
    }
}
