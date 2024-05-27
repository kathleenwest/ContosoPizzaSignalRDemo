namespace ContosoPizza.Models
{
    /// <summary>
    /// Simple Pizza
    /// </summary>
    public class Pizza
    {
        public Guid id { get; set; }
        
        /// <summary>
        /// Name of the Pizza (string)
        /// </summary>
        public string Name { get; set; }

        public Pizza()
        {
            id = Guid.NewGuid();
        }

    } // end of class

} // end of namespace
