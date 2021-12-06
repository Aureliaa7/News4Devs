using System.Threading.Tasks;

namespace News4Devs.Shared.Interfaces.Services
{
    public interface IImageService
    {
        /// <summary>
        /// Saves an image
        /// </summary>
        /// <param name="imageContent"></param>
        /// <returns>The image name</returns>
        Task<string> SaveImageAsync(byte[] imageContent);
    }
}
