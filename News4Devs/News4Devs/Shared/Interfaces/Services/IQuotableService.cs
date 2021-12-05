using News4Devs.Core.DTOs;
using System.Threading.Tasks;

namespace News4Devs.Core.Interfaces.Services
{
    public interface IQuotableService
    {
        Task<QuotableApiResponseDto> GetRandomQuoteAsync();
    }
}
