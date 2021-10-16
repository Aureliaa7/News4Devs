using System.Threading.Tasks;

namespace News4Devs.Core.Interfaces.Services
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(byte[] imageContent);
    }
}
