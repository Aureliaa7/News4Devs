using System.IO;
using Blazored.Toast.Services;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using News4Devs.Core.DTOs;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;
using News4Devs.Client.Helpers;
using System.Net;

namespace News4Devs.Client.Pages.Accounts
{ 
    public partial class Register : ComponentBase
    {
        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private HttpClient HttpClient { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private RegisterDto registerModel = new();
        private IBrowserFile imageFile;

        private async Task CreateAccount()
        {
            var profilePhotoContent = await GetProfilePhotoContentAsync();
            if (profilePhotoContent != null)
            {
                registerModel.ProfilePhotoContent = profilePhotoContent;
            }

            var byteArrayContent = ByteArrayContentHelper.ConvertToByteArrayContent(registerModel);
            var result = await this.HttpClient.PostAsync("accounts/register", byteArrayContent);

            if (result.StatusCode == HttpStatusCode.Created)
            {
                NavigationManager.NavigateTo("/login");
            }
            else if (result.StatusCode == HttpStatusCode.Conflict)
            {
                ToastService.ShowError("A user with the same email already exists!");
            }

            registerModel = new(); // clear the form


            //TODO find some cute background images
        }

        private void HandleSelected(InputFileChangeEventArgs e)
        {
            imageFile = e.File;
        }

        private async Task<byte[]> GetProfilePhotoContentAsync()
        {
            if (imageFile != null)
            {
                var resizedFile = await imageFile.RequestImageFileAsync(
                    "image/png", ClientConstants.ImageWidth, ClientConstants.ImageHeight);
                using (var source = resizedFile.OpenReadStream(resizedFile.Size))
                {
                    using (var stream = new MemoryStream())
                    {
                        await source.CopyToAsync(stream);
                        return stream.ToArray();
                    }
                }
            }
            return null;
        }
    }
}
