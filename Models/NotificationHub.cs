using Microsoft.AspNetCore.SignalR;

namespace ProductManagementWebAPI.Models
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string user, string message)
        {
            await Clients.All.SendAsync("ReceivedMessage", user, message);
        }
    }
}
