using Microsoft.AspNetCore.Components;
using News4Devs.Client.Services.Interfaces;
using News4Devs.Shared.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace News4Devs.Client.Components.Accounts
{
    public partial class Contacts
    {
        [Inject]
        private IHttpClientService HttpService { get; set; }

        [Inject]
        protected IAuthenticationService AuthService { get; set; }

        private bool isLoadMoreButtonVisible = false;
        private int currentPage = 1;
        private string currentUserId;
        private bool isLoading = false;

        private List<UserDto> Users { get; set; } = new List<UserDto>();

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            currentUserId = await AuthService.GetCurrentUserIdAsync();
            await GetUsersAsync(currentUserId);
            isLoading = false;
        }

        private async Task OnLoadMore()
        {
            await GetUsersAsync(currentUserId);
        }

        private async Task GetUsersAsync(string currentUserId)
        {
            var response = await HttpService.GetAsync<PagedResponseDto<UserDto>>(
                $"{ClientConstants.BaseUrl}/accounts?pageNumber={currentPage}&pageSize={ClientConstants.MaxPageSize}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var pagedResponse = response.Data;
                Users.AddRange(pagedResponse.Data.Where(x => x.Id.ToString() != currentUserId));
                isLoadMoreButtonVisible = pagedResponse.NextPage != null;

                if (currentPage < pagedResponse.TotalPages)
                {
                    currentPage++;
                }
            }
        }
    }
}
