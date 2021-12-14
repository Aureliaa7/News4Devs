using Microsoft.AspNetCore.SignalR;
using News4Devs.Shared.DTOs;
using System.Threading.Tasks;

namespace News4Devs.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(ChatMessageDto message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
