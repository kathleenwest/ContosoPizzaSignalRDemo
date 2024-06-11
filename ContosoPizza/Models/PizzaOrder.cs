using ContosoPizza.Services;
using Microsoft.AspNetCore.SignalR;

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
                    UpdatePizzaOrderAsync();
                }
            }
        }

        private void UpdatePizzaOrderAsync()
        {
            switch (Status)
            {
                case OrderStatus.Received:
                    // Retrieve the pizza
                    PizzaOrder? pizzaOrder = _orderService.Get(OrderId);

                    // Validate the pizza exists
                    if (pizzaOrder == null)
                    {
                        // Order the Pizza
                        _orderService.OrderPizza(this);
                    }
                    break;
                case OrderStatus.Preparation:
                    _orderService.PreparePizza(this);
                    break;
                case OrderStatus.Baking:
                    _orderService.BakePizza(this);
                    break;
                case OrderStatus.OutForDelivery:
                    _orderService.DeliverPizza(this);
                    break;
                case OrderStatus.Complete:
                    _orderService.CompleteOrder(this);
                    break;
                default:
                    break;
            }

            //if(Status != OrderStatus.New)
            //{
            //    // Send the client an order update
            //    await _hubContext.Clients.Client(ConnectionId).SendAsync("ReceiveOrderUpdate", "Pizza Order Status Update: " + Status);
            //}
          
        }

        public PizzaOrder()
        {
            
            OrderId = Guid.NewGuid();
        }
       
    }

}
