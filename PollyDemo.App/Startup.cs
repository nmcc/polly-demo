using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Caching.Memory;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using PollyDemo.App.CircuitBreaker;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace PollyDemo.App
{
    public class Startup
    {
        private readonly ILogger<Startup> logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            this.logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Declare the ApiClient singletong
            services.AddSingleton<ApiClient>();

            // Declare the resilient ApiClient
            var memoryCacheProvider = new MemoryCacheProvider(new MemoryCache(new MemoryCacheOptions()));
            services.AddSingleton(memoryCacheProvider);
            services.AddSingleton<ResilientApiClient>();

            // Using Polly.Extensions.Http way of doing things
            services.AddHttpClient("PollyDemo.Server", client =>
                {
                    client.BaseAddress = new Uri(Configuration.GetValue<string>("Server:BaseUrl"));
                    client.Timeout = TimeSpan.FromSeconds(1);
                })
                .AddPolicyHandler(GetCircuitBreakerPolicy());
        }

        private IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30),
                    onBreak: (error, retryIn) => logger.LogWarning($"Preventing connections for {retryIn.TotalSeconds} seconds"),
                    onReset: () => logger.LogWarning("Connection is restablished"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
