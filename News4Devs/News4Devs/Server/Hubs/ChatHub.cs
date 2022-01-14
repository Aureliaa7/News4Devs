using Microsoft.AspNetCore.SignalR;
using News4Devs.Shared.DTOs;
using System.Threading.Tasks;

namespace News4Devs.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessageAsync(ChatMessageDto message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task NotifyAsync(string message, string receiverId, string senderId)
        {
            await Clients.All.SendAsync("NewMessageNotification", message, receiverId, senderId);
        }
    }
}
