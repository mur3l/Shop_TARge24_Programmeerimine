using Microsoft.AspNetCore.SignalR;

namespace ShopTARge24.Hubs
{
    public class UserHub : Hub
    {
        public static int TotalViews { get; set; } = 0;
        public static int TotalUsers { get; set; } = 0;

        public override async Task OnConnectedAsync()
        {
            TotalUsers++;
            await Clients.All.SendAsync("updateTotalUsers", TotalUsers);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            TotalUsers--;
            await Clients.All.SendAsync("updateTotalUsers", TotalUsers);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task NewWindowLoaded()
        {
            TotalViews++;
            await Clients.All.SendAsync("updateTotalViews", TotalViews);
        }
    }
}
