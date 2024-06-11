using ContosoPizza.Services;

namespace ContosoPizza.Models
{
    public class PizzaOrder
    {       
        private OrderStatus orderStatus = default;
        private IOrderService _orderService { get; set; } = SingletonService.GetInstanceIOrderService;
        public Guid OrderId { get; set; }
        public string CustomerName { get; set; } = "Pizza Customer";
        public Pizza Pizza { get; set; } 
        public string ConnectionId { get; set; } 
        public OrderStatus Status
        {
            get => orderStatus;
            set
            {
                if (value != orderStatus)
                {
                    orderStatus = value;
                    _orderService.UpdatePizzaOrderAsync(this);
                }
            }
        }

        public PizzaOrder()
        {          
            OrderId = Guid.NewGuid();
        }
       
    }

}
