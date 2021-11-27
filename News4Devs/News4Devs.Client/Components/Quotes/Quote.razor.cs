using Microsoft.AspNetCore.Components;
using News4Devs.Client.Services.Interfaces;
using News4Devs.Core.DTOs;
using System.Net;
using System.Threading.Tasks;

namespace News4Devs.Client.Components.Quotes
{
    public partial class Quote
    {
        [Inject]
        private IHttpClientService HttpClientService { get; set; }

        private QuotableApiResponseDto quoteDto { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var apiResponse = await HttpClientService.GetAsync<QuotableApiResponseDto>($"{ClientConstants.BaseUrl}v1/quotes/random");
            if (apiResponse.StatusCode == HttpStatusCode.OK)
            {
                quoteDto = apiResponse.Data;
            }
            else
            {
                quoteDto = new QuotableApiResponseDto { author = string.Empty, content = string.Empty };
            }
        }

    }
}
