using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using News4Devs.Client.Services.Interfaces;
using News4Devs.Shared.DTOs;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace News4Devs.Client.Components.Books
{
    public partial class ITBooks
    {
        [Inject]
        private IHttpClientService HttpClientService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }


        private IList<BookDto> Books = new List<BookDto>();
        private bool loading;

        protected override async Task OnInitializedAsync()
        {
            loading = true;
            var apiResponse = await HttpClientService.GetAsync<IList<BookDto>>($"{ClientConstants.BaseUrl}/books/new");
            if (apiResponse.StatusCode == HttpStatusCode.OK)
            {
                Books = apiResponse.Data;
            }
            else
            {
                ToastService.ShowError("Could not retrieve the books...");
            }
            loading = false;
        }
    }
}
