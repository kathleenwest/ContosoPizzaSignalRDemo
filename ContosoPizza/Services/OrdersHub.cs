using Microsoft.AspNetCore.SignalR;

namespace ContosoPizza.Services
{
    public class OrdersHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            // You can now use this connection ID to track the client
            return base.OnConnectedAsync();
        }

        // TODO We are going to send kitchen open, kitchen closed notifications
        public async Task BroadcastMessageAllClients(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }

        public async Task UpdateClientAboutOrder(string connectionId, string updateMessage)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveOrderUpdate", updateMessage);
        }
    }
}
