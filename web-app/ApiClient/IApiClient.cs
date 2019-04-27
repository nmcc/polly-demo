using System;
using System.Threading.Tasks;

namespace PollyDemo.App.CircuitBreaker
{
    public interface IApiClient : IDisposable
    {
        string BaseUrl { get; set; }

        Task<byte[]> GetAvatarAsync(string name);

        bool IsHealthy { get; }

        bool IsCircuitClosed { get; }
    }
}