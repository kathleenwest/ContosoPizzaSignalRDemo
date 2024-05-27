using Microsoft.AspNetCore.SignalR;

namespace ContosoPizza.Services
{
    public class OrdersHub : Hub
    {
        public async Task BroadcastOrderUpdate(string orderStatus)
        {
            await Clients.All.SendAsync("ReceiveOrderUpdate", orderStatus);
        }
    }

}
