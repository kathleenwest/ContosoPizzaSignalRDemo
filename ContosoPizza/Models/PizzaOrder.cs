namespace ContosoPizza.Models
{
    public class PizzaOrder
    {
        public Guid OrderId { get; set; }
        public string CustomerName { get; set; } = "Pizza Customer";
        public Pizza Pizza { get; set; }
        public string ConnectionId { get; set; }
        public OrderStatus Status { get; set; }

        public PizzaOrder()
        {
            OrderId = Guid.NewGuid();
            Status = OrderStatus.Received;
        }
       
    }

}
