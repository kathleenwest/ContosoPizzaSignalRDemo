using ContosoPizza.Models;
using Microsoft.AspNetCore.SignalR;

namespace ContosoPizza.Services
{
    public interface IOrderService
    {
        Task OrderPizzaAsync(PizzaOrder order);
        void PreparePizza(PizzaOrder order);
        void BakePizza(PizzaOrder order);
        void DeliverPizza(PizzaOrder order);
        void CompleteOrder(PizzaOrder order);
        List<PizzaOrder> GetAllPizzaOrders();
        PizzaOrder? Get(Guid id);
        void Update(PizzaOrder pizzaOrder);
        Task UpdatePizzaOrderAsync(PizzaOrder order);
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

        public void PreparePizza(PizzaOrder order)
        {
            // Update the Pizza Order Status
            order.Status = OrderStatus.Preparation;
        }

        public void BakePizza(PizzaOrder order)
        {
            // Update the Pizza Order Status
            order.Status = OrderStatus.Baking;
        }

        public void DeliverPizza(PizzaOrder order)
        {
            // Update the Pizza Order Status
            order.Status = OrderStatus.OutForDelivery;
        }

        public void CompleteOrder(PizzaOrder order)
        {
            // Update the Pizza Order Status
            order.Status = OrderStatus.Complete;
        }

        public List<PizzaOrder> GetAllPizzaOrders()
        {
            return PizzaOrders;
        }

        public PizzaOrder? Get(Guid id) => PizzaOrders.FirstOrDefault(p => p.OrderId == id);

        public void Update(PizzaOrder pizzaOrder)
        {
            int index = PizzaOrders.FindIndex(p => p.OrderId == pizzaOrder.OrderId);

            if (index == -1)
                return;

            PizzaOrders[index] = pizzaOrder;
        }

        public async Task UpdatePizzaOrderAsync(PizzaOrder order)
        {
            switch (order.Status)
            {
                case OrderStatus.Received:
                    // Retrieve the pizza
                    PizzaOrder? pizzaOrder = Get(order.OrderId);

                    // Validate the pizza exists
                    if (pizzaOrder == null)
                    {
                        // Order the Pizza
                        await OrderPizzaAsync(order);
                    }
                    break;
                case OrderStatus.Preparation:
                    PreparePizza(order);
                    break;
                case OrderStatus.Baking:
                    BakePizza(order);
                    break;
                case OrderStatus.OutForDelivery:
                    DeliverPizza(order);
                    break;
                case OrderStatus.Complete:
                    CompleteOrder(order);
                    break;
                default:
                    break;
            }

            if (order.Status != OrderStatus.New)
            {
                // Send the client an order update
                await _hubContext.Clients.Client(order.ConnectionId).SendAsync("ReceiveOrderUpdate", "Pizza Order Status Update: " + order.Status);
            }

        }

    }
}
