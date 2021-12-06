using News4Devs.Shared.DTOs;
using News4Devs.Shared.Models;
using System.Threading.Tasks;

namespace News4Devs.Shared.Interfaces.Services
{
    public interface IQuotableService
    {
        Task<QuotableApiResponseDto> GetRandomQuoteAsync(QuotableApiQueryParamsModel queryParamsModel);
    }
}
