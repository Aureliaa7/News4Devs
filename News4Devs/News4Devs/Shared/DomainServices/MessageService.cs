using News4Devs.Shared.Entities;
using News4Devs.Shared.Exceptions;
using News4Devs.Shared.Interfaces.Services;
using News4Devs.Shared.Interfaces.UnitOfWork;
using News4Devs.Shared.Pagination;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace News4Devs.Shared.DomainServices
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMessagePaginationService messagePaginationService;

        public MessageService(IUnitOfWork unitOfWork, IMessagePaginationService messagePaginationService)
        {
            this.unitOfWork = unitOfWork;
            this.messagePaginationService = messagePaginationService;
        }

        public async Task<PagedResponseModel<ChatMessage>> GetConversationAsync(Guid fromUserId, Guid toUserId, PaginationFilter paginationFilter)
        {
            Expression<Func<ChatMessage, bool>> filter = x => x.FromUserId == fromUserId && x.ToUserId == toUserId ||
                x.FromUserId == toUserId && x.ToUserId == fromUserId;
            var result = await messagePaginationService.GetPagedResponseAsync($"{Constants.ChatAddress}/{fromUserId}/{toUserId}", paginationFilter, filter);

            return result;
        }

        public async Task<ChatMessage> SaveMessageAsync(ChatMessage message)
        {
            bool senderUserExists = await unitOfWork.UsersRepository.ExistsAsync(x => x.Id == message.FromUserId);
            bool receiverUserExists = await unitOfWork.UsersRepository.ExistsAsync(x => x.Id == message.ToUserId);

            if (!senderUserExists && !receiverUserExists)
            {
                throw new EntityNotFoundException($"Couldn't save the message since the users with the " +
                    $"ids '{message.FromUserId}', '{message.ToUserId}' were not found... ");
            }

            message.CreatedDate = DateTime.Now;

            await unitOfWork.MessagesRepository.AddAsync(message);
            await unitOfWork.SaveChangesAsync();

            return message;
        }
    }
}
