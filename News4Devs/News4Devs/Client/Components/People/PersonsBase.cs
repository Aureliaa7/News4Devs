using Microsoft.AspNetCore.Components;
using News4Devs.Client.Services.Interfaces;
using News4Devs.Shared.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace News4Devs.Client.Components.People
{
    public abstract class PersonsBase: ComponentBase
    {
        [Inject]
        protected IHttpClientService HttpService { get; set; }

        [Inject]
        protected IAuthenticationService AuthService { get; set; }

        protected bool isLoadMoreButtonVisible = false;
        protected int currentPage = 1;
        protected string currentUserId;
        protected bool isLoading;
        protected List<UserDto> users = new();

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            currentUserId = await AuthService.GetCurrentUserIdAsync();
            await GetUsersAsync(currentUserId);
            isLoading = false;
        }

        protected async Task GetUsersAsync(string currentUserId)
        {
            string url = GetUrl();
            var response = await HttpService.GetAsync<PagedResponseDto<UserDto>>(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var pagedResponse = response.Data;
                users.AddRange(pagedResponse.Data.Where(x => x.Id.ToString() != currentUserId));
                isLoadMoreButtonVisible = pagedResponse.NextPage != null;

                if (currentPage < pagedResponse.TotalPages)
                {
                    currentPage++;
                }
            }
        }

        protected async Task LoadMoreUsers()
        {
            await GetUsersAsync(currentUserId);
        }

        protected abstract string GetUrl();
    }
}
