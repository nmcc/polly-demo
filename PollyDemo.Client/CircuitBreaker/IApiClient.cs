using System.Threading.Tasks;

namespace PollyDemo.Client.CircuitBreaker
{
    interface IApiClient
    {
        Task<string> SayHelloAsync(string name);
    }
}