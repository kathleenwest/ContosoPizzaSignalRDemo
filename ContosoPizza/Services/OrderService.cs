using ContosoPizza.Models;
using Microsoft.AspNetCore.SignalR;

namespace ContosoPizza.Services
{
    public interface IOrderService
    {
        Task OrderPizzaAsync(PizzaOrder order);
        List<PizzaOrder> GetAllPizzaOrders();
        PizzaOrder? GetPizzaOrder(Guid id);
        void UpdatePizzaOrder(PizzaOrder pizzaOrder);
        Task SendCustomerOrderStatusUpdateAsync(PizzaOrder order);
    }

    public class OrderService : IOrderService
    {
        /// <summary>
        /// In-memory static list of PizzaOrders
        /// (mock of database later)
        /// </summary>
        private static List<PizzaOrder> PizzaOrders { get; set; } = new List<PizzaOrder>();

        private readonly IHubContext<OrdersHub> _hubContext;

        public OrderService(IHubContext<OrdersHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task OrderPizzaAsync(PizzaOrder order)
        {           
            // Add to the Pizza Order
            PizzaOrders.Add(order);

            // Update the Pizza Order Status
            order.Status = OrderStatus.Received;

            // Send the admin with the new order
            await _hubContext.Clients.All.SendAsync("ReceiveAdminOrderUpdate", "New Pizza Order From Customer: " + order.CustomerName);
        }

        public List<PizzaOrder> GetAllPizzaOrders()
        {
            return PizzaOrders;
        }

        public PizzaOrder? GetPizzaOrder(Guid id) => PizzaOrders.FirstOrDefault(p => p.OrderId == id);

        public void UpdatePizzaOrder(PizzaOrder pizzaOrder)
        {
            int index = PizzaOrders.FindIndex(p => p.OrderId == pizzaOrder.OrderId);

            if (index == -1)
                return;

            PizzaOrders[index] = pizzaOrder;
        }

        public async Task SendCustomerOrderStatusUpdateAsync(PizzaOrder order)
        {
            if (order.Status != OrderStatus.New)
            {
                // Send the client an order update
                await _hubContext.Clients.Client(order.ConnectionId).SendAsync("ReceiveOrderUpdate", "Pizza Order Status Update: " + order.Status);
            }
        }

    }
}
