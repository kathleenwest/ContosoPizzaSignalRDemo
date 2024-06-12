using Microsoft.AspNetCore.SignalR;
using System.Xml.Linq;

namespace ContosoPizza.Services
{
    public class OrdersHub : Hub
    {
        public async Task BroadcastMessageAllClients(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }

        public async Task UpdateClientAboutOrder(string updateMessage)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveOrderUpdate", updateMessage);
        }

        public async Task UpdateAdminAboutNewOrder(string updateMessage)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveAdminOrderUpdate", updateMessage);
        }

    }
}
