namespace ContosoPizza.Models
{
    /// <summary>
    /// Lifecycle of Pizza Order
    /// </summary>
    public enum OrderStatus
    {
        New = 0,
        Received = 1,
        Preparation = 2,
        Baking = 3,
        OutForDelivery = 4,
        Complete = 5
    }
}
