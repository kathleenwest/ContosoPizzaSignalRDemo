using ContosoPizza.Models;
using Microsoft.AspNetCore.SignalR;

namespace ContosoPizza.Services
{
    public interface IOrderService
    {
        /// <summary>
        /// Order a Pizza
        /// </summary>
        /// <param name="order">(PizzaOrder) pizza order</param>
        /// <returns>a Task without result</returns>
        Task OrderPizzaAsync(PizzaOrder order);

        /// <summary>
        /// Returns all Pizza Orders
        /// </summary>
        /// <returns>List of (PizzaOrder) objects</returns>
        List<PizzaOrder> GetAllPizzaOrders();

        /// <summary>
        /// Get Pizza Order
        /// </summary>
        /// <param name="id">Guid of the known pizza order</param>
        /// <returns>a (PizzaOrder) object (if existing)</returns>
        PizzaOrder? GetPizzaOrder(Guid id);

        /// <summary>
        /// Updates the Existing Pizza Order
        /// </summary>
        /// <param name="pizzaOrder">existing (PizzaOrder) order</param>
        void UpdatePizzaOrder(PizzaOrder pizzaOrder);

        /// <summary>
        /// Send Pizza Order Update to Customer
        /// </summary>
        /// <param name="order">(PizzaOrder) order</param>
        /// <returns>a Task without results</returns>
        Task SendCustomerOrderStatusUpdateAsync(PizzaOrder order);
    }

    public class OrderService : IOrderService
    {
        /// <summary>
        /// In-memory static list of PizzaOrders
        /// (mock of database later)
        /// </summary>
        private static List<PizzaOrder> PizzaOrders { get; set; } = new List<PizzaOrder>();

        /// <summary>
        /// Reference to the IHubContext (SignalR)
        /// </summary>
        private readonly IHubContext<OrdersHub> _hubContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hubContext">IHubContext of type OrdersHub</param>
        public OrderService(IHubContext<OrdersHub> hubContext)
        {
            _hubContext = hubContext;
        }

        /// <summary>
        /// Order a Pizza
        /// </summary>
        /// <param name="order">(PizzaOrder) pizza order</param>
        /// <returns>a Task without result</returns>
        public async Task OrderPizzaAsync(PizzaOrder order)
        {           
            // Add to the Pizza Orders
            PizzaOrders.Add(order);

            // Update the Pizza Order Status
            order.Status = OrderStatus.Received;

            // Send the pizza store admin with the new order
            await _hubContext.Clients.All.SendAsync("ReceiveAdminOrderUpdate", "New Pizza Order From Customer: " + order.CustomerName);
        }

        /// <summary>
        /// Returns all Pizza Orders
        /// </summary>
        /// <returns>List of (PizzaOrder) objects</returns>
        public List<PizzaOrder> GetAllPizzaOrders()
        {
            return PizzaOrders;
        }

        /// <summary>
        /// Get Pizza Order
        /// </summary>
        /// <param name="id">Guid of the known pizza order</param>
        /// <returns>a (PizzaOrder) object (if existing)</returns>
        public PizzaOrder? GetPizzaOrder(Guid id) => PizzaOrders.FirstOrDefault(p => p.OrderId == id);

        /// <summary>
        /// Updates the Existing Pizza Order
        /// </summary>
        /// <param name="pizzaOrder">existing (PizzaOrder) order</param>
        public void UpdatePizzaOrder(PizzaOrder pizzaOrder)
        {
            // Find the existing pizza order
            int index = PizzaOrders.FindIndex(p => p.OrderId == pizzaOrder.OrderId);

            // Pizza not found
            if (index == -1)
                return;

            // Update the Pizza order
            PizzaOrders[index] = pizzaOrder;
        }

        /// <summary>
        /// Send Pizza Order Update to Customer
        /// </summary>
        /// <param name="order">(PizzaOrder) order</param>
        /// <returns>a Task without results</returns>
        public async Task SendCustomerOrderStatusUpdateAsync(PizzaOrder order)
        {
            if (order.Status != OrderStatus.New)
            {
                // Send the client an order update
                await _hubContext.Clients.Client(order.ConnectionId).SendAsync("ReceiveOrderUpdate", "Pizza Order Status Update: " + order.Status);
            }
        }

    } // end of class
} // end of namespace
