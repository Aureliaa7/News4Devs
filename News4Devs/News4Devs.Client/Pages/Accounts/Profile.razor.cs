using Microsoft.AspNetCore.Components;
using News4Devs.Client.Services.Interfaces;
using News4Devs.Core.DTOs;
using System.Threading.Tasks;

namespace News4Devs.Client.Pages.Accounts
{
    public partial class Profile
    {
        [Inject]
        private IHttpClientService HttpClientService { get; set; }

        [Parameter]
        public string Id { get; set; }

        private UserDto userDetails;
        private string imageSrc;
        private string fullName;

        protected override async Task OnInitializedAsync()
        {
            var response = await HttpClientService.GetAsync<UserDto>($"v1/accounts/{Id}");
            userDetails = response.Data;
            fullName = $"{userDetails.FirstName} {userDetails.LastName}";
            imageSrc = ClientConstants.ProfileImagesDirector + userDetails?.ProfilePhotoName;
        }
    }
}
