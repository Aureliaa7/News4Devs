using Microsoft.AspNetCore.Components;
using News4Devs.Client.Services.Interfaces;
using News4Devs.Shared.DTOs;
using System.Threading.Tasks;

namespace News4Devs.Client.Components.Accounts
{
    public partial class Profile
    {
        [Inject]
        private IHttpClientService HttpClientService { get; set; }

        [Parameter]
        public string Id { get; set; }

        private UserDto userDetails;
        private string fullName;
        private string imageSrc;

        protected override async Task OnInitializedAsync()
        {
            var response = await HttpClientService.GetAsync<UserDto>($"v1/accounts/{Id}");
            userDetails = response.Data;
            fullName = $"{userDetails.FirstName} {userDetails.LastName}";
            // since I cannot use IWebHostEnvironment to get the wwwroot path, create the image path this way
            imageSrc = $"~/../profile-photos/{userDetails.ProfilePhotoName}";
        }
    }
}
