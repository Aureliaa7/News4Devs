using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using News4Devs.Client.Services;
using News4Devs.Client.Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace News4Devs.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            RegisterServices(builder);

            await builder.Build().RunAsync();
        }

        private static void RegisterServices(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(ClientConstants.BaseUrl) });

            builder.Services.AddBlazoredToast();

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddScoped<IHttpClientService, HttpClientService>();
        }
    }
}
