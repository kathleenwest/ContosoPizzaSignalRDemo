namespace ContosoPizza.Models
{
    /// <summary>
    /// Simple Pizza
    /// </summary>
    public class Pizza
    {
        public Guid id { get; set; }
        
        public string PizzaDetails { get; set; }

        public Pizza()
        {
            id = Guid.NewGuid();
        }

    } // end of class

} // end of namespace
