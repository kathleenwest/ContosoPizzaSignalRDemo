namespace ContosoPizza.Models
{
    /// <summary>
    /// Simple Pizza
    /// </summary>
    public class Pizza
    {
        /// <summary>
        /// Id
        /// Globally unique identifier for the pizza
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Pizza Details: What the customer wants
        /// </summary>
        public string PizzaDetails { get; set; }

        /// <summary>
        /// Construtor
        /// When a new pizza is created, it is assigned a guid
        /// </summary>
        public Pizza()
        {
            Id = Guid.NewGuid();
        }

    } // end of class
} // end of namespace
