using Microsoft.AspNetCore.SignalR;

namespace ProductManagementWebAPI.Models
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string user, string message, int? productId = null)
        {
            await Clients.Others.SendAsync("ReceivedNotification", user, message, productId);
        }
    }
}
