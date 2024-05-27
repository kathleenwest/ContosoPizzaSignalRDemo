using ContosoPizza.Models;
using Microsoft.AspNetCore.SignalR;

namespace ContosoPizza.Services
{
    public interface IOrderService
    {
        Task OrderPizza(PizzaOrder order);
        Task PreparePizza(PizzaOrder order);
        Task BakePizza(PizzaOrder order);
        Task DeliverPizza(PizzaOrder order);
    }


    public class OrderService : IOrderService
    {
        OrdersHub hub { get; set; }

        /// <summary>
        /// In-memory static list of PizzaOrders
        /// (mock of database later)
        /// </summary>
        static internal List<PizzaOrder> PizzaOrders { get; set; }

        public OrderService()
        {
            hub = new OrdersHub();
        }

        public async Task OrderPizza(PizzaOrder order)
        {
            // Order Pizza
            // Send Update
            return;
        }

        public async Task PreparePizza(PizzaOrder order)
        {
            // Order Pizza
            // Send Update
            return;
        }

        public async Task BakePizza(PizzaOrder order)
        {
            // Order Pizza
            // Send Update
            return;
        }

        public async Task DeliverPizza(PizzaOrder order)
        {
            // Deliver Pizza

            return;
        }

    }
}
