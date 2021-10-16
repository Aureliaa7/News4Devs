using News4Devs.Core.Interfaces.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace News4Devs.Core.DomainServices
{
    public class ImageService : IImageService
    {
        public async Task<string> SaveImageAsync(byte[] imageContent)
        {
            string imageFolder = Constants.ProfileImagesPath;
            // let it be a simple GUID since the image name is not important
            string fileName = Guid.NewGuid().ToString() + Constants.ImageExtension;
            string filePath = Path.Combine(imageFolder, fileName);
            await File.WriteAllBytesAsync(filePath, imageContent);

            return filePath;
        }
    }
}
