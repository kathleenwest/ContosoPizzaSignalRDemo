using Microsoft.AspNetCore.SignalR;
using System.Xml.Linq;

namespace ContosoPizza.Services
{
    public class OrdersHub : Hub
    {
        //public override Task OnConnectedAsync()
        //{
        //    string connectionId = Context.ConnectionId;

        //    return base.OnConnectedAsync();
        //}

        //public override Task OnDisconnectedAsync(Exception? exception)
        //{
        //    return base.OnDisconnectedAsync(exception);
        //}

        public async Task BroadcastMessageAllClients(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }

        public async Task UpdateClientAboutOrder(string updateMessage)
        {
            //await Clients.Client(connectionId).SendAsync("ReceiveOrderUpdate", updateMessage);
            //await Clients.Caller.SendAsync("ReceiveOrderUpdate", Context.ConnectionId, updateMessage);
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveOrderUpdate", updateMessage);
        }

    }
}
