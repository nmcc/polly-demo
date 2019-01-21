using Microsoft.Extensions.Configuration;
using System;

namespace PollyDemo.Client
{
    internal class Settings
    {
        private static Lazy<Settings> instance = new Lazy<Settings>(() => new Settings());

        private Settings()
        {
            var configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            this.BaseUrl = configurationRoot.GetSection("Server:BaseUrl").Value;
        }

        public string BaseUrl { get; }

        public static Settings Instance => instance.Value;
    }
}
