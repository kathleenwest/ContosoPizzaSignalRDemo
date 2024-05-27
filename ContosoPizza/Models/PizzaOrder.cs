namespace ContosoPizza.Models
{
    public class PizzaOrder
    {
        public Guid OrderId { get; set; }
        public string CustomerName { get; set; } = "Pizza Customer";
        public List<Pizza> Pizzas { get; set; }
        public string ConnectionId { get; set; }
        public OrderStatus Status { get; set; }

        public PizzaOrder()
        {
            OrderId = Guid.NewGuid();
            Pizzas = new List<Pizza>();
            Status = OrderStatus.Received;
        }
       
    }

}
