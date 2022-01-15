using Microsoft.AspNetCore.Components;
using News4Devs.Client.Services.Interfaces;
using System.Threading.Tasks;

namespace News4Devs.Client.Components.Accounts
{
    public partial class CurrentUser
    {
        [Inject]
        private IAuthenticationService AuthService { get; set; }

        private string currentUserName;
        private string currentUserId;

        protected override async Task OnInitializedAsync()
        {
            currentUserName = await AuthService.GetCurrentUserFullNameAsync();
            currentUserId = await AuthService.GetCurrentUserIdAsync();
        }
    }
}
