using News4Devs.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News4Devs.Core.Interfaces.Services
{
    public interface IITBookstoreService
    {
        Task<IList<BookDto>> GetNewBooksAsync();
    }
}
