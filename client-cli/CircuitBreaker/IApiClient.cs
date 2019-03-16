namespace PollyDemo.Client.CircuitBreaker
{
    interface IApiClient
    {
        string SayHello(string name);
    }
}