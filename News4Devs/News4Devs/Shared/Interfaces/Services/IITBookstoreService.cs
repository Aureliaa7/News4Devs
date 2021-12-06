using News4Devs.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News4Devs.Shared.Interfaces.Services
{
    public interface IITBookstoreService
    {
        Task<IList<BookDto>> GetNewBooksAsync();
    }
}
