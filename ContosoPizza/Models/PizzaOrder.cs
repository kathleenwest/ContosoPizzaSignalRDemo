using ContosoPizza.Services;

namespace ContosoPizza.Models
{
    /// <summary>
    /// Simple Pizza Order Class Model
    /// </summary>
    public class PizzaOrder
    {   
        /// <summary>
        /// Private field for Status property
        /// </summary>
        private OrderStatus orderStatus = default;
        
        /// <summary>
        /// Reference to the IOrderService
        /// </summary>
        private IOrderService _orderService { get; set; } = SingletonService.GetInstanceIOrderService;

        /// <summary>
        /// Globably unique identifier for the the order
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// The customer name for the pizza order (submitted)
        /// </summary>
        public string CustomerName { get; set; } = "Pizza Customer";

        /// <summary>
        /// Pizza model reference for the order
        /// </summary>
        public Pizza Pizza { get; set; } 

        /// <summary>
        /// SignalR ConnectionId for this client
        /// </summary>
        public string ConnectionId { get; set; } 

        /// <summary>
        /// Current status of the pizza order
        /// </summary>
        public OrderStatus Status
        {
            get => orderStatus;
            set
            {
                // Upon change we want to trigger some action and
                // also communicate the order status to the connected customer client
                if (value != orderStatus)
                {
                    orderStatus = value;
                    _orderService.SendCustomerOrderStatusUpdateAsync(this);
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public PizzaOrder()
        {
            // Upon creation, assign a globally uniqie identifier for the order
            OrderId = Guid.NewGuid();
        }
       
    } // end of class
} // end of namespace
