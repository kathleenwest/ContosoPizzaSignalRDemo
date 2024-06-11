namespace ContosoPizza.Services
{
    public static class SingletonService
    {
        private static IOrderService _orderService;

        public static void SetInstance(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public static IOrderService GetInstanceIOrderService => _orderService;

    }
}
