namespace ContosoPizza.Models
{
    /// <summary>
    /// Lifecycle of Pizza Order
    /// </summary>
    public enum OrderStatus
    {
        New,
        Received,
        Preparation,
        Baking,
        OutForDelivery,
        Complete
    }
}
