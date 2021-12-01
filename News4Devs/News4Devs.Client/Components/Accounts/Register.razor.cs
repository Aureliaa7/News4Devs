using System.IO;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using News4Devs.Core.DTOs;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;
using System.Net;
using News4Devs.Client.Services.Interfaces;
using News4Devs.Client.Helpers;

namespace News4Devs.Client.Components.Accounts
{ 
    public partial class Register
    {
        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private IHttpClientService HttpClientService { get; set; }

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
            var apiResponse = await HttpClientService.PostAsync<UserDto>("v1/accounts/register", byteArrayContent);

            if (apiResponse.StatusCode == HttpStatusCode.Created)
            {
                NavigationManager.NavigateTo("/login");
            }
            else if (apiResponse.StatusCode == HttpStatusCode.Conflict)
            {
                ToastService.ShowError("A user with the same email already exists!");
            }

            registerModel = new(); 
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
