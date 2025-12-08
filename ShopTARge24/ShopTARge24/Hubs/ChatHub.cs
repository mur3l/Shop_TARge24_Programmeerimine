using Microsoft.AspNetCore.SignalR;

namespace ShopTARge24.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message, string time)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message, time);
        }
    }
}
