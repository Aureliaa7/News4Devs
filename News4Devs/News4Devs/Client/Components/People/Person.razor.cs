using Microsoft.AspNetCore.Components;
using News4Devs.Shared.DTOs;

namespace News4Devs.Client.Components.People
{
    public partial class Person
    {
        [Parameter]
        public UserDto User { get; set; }

        private string imageSrc;

        protected override void OnInitialized()
        {
            imageSrc = $"~/../profile-photos/{User.ProfilePhotoName}";
        }
    }
}
