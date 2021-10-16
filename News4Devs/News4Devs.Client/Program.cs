using Blazored.Toast;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
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

            var baseAddress = new Uri("https://localhost:44347/api/");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = baseAddress });

            // register BlazoredToast
            builder.Services.AddBlazoredToast();

            await builder.Build().RunAsync();
        }
    }
}
