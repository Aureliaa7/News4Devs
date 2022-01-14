using News4Devs.Shared.Entities;
using News4Devs.Shared.Models;
using System;
using System.Threading.Tasks;

namespace News4Devs.Shared.Interfaces.Services
{
    public interface IMessageService
    {
        Task<ChatMessage> SaveMessageAsync(ChatMessage message);

        Task<PagedResponseModel<ChatMessage>> GetConversationAsync(Guid fromUserId, Guid toUserId, PaginationFilter paginationFilter);
    }
}
