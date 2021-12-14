using News4Devs.Shared.Entities;
using News4Devs.Shared.Interfaces.Services;
using News4Devs.Shared.Interfaces.UnitOfWork;
using News4Devs.Shared.Pagination;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace News4Devs.Shared.DomainServices
{
    public class MessagePaginationService : PaginationServiceBase<ChatMessage, ChatMessage>, IMessagePaginationService
    {
        private readonly IUnitOfWork unitOfWork;

        public MessagePaginationService(IUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;
        }

        public async override Task<PagedResponseModel<ChatMessage>> GetPagedResponseAsync(
            string address, 
            PaginationFilter paginationFilter, 
            Expression<Func<ChatMessage, bool>> filter = null)
        {
            int totalRecords = await unitOfWork.MessagesRepository.GetTotalRecordsAsync(filter);

            //order them in descending order(that means the last sent message would be now the 1st),
            // take x and then order those messages in ascending order
            var messages = (await unitOfWork.MessagesRepository.GetAllAsync(filter))
                .OrderByDescending(x => x.CreatedDate)
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize)
                .OrderBy(x => x.CreatedDate)
                .ToList();

            var pagedResponse = GetPagedResponseModel(messages, address, totalRecords, paginationFilter);
            return pagedResponse;
        }
    }
}
