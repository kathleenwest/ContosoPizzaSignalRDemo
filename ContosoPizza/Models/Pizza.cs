﻿namespace ContosoPizza.Models
{
    /// <summary>
    /// Simple Pizza Model
    /// </summary>
    public class Pizza
    {
        /// <summary>
        /// Globally unique identifier for the pizza
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Pizza Details: What the customer wants
        /// </summary>
        public string PizzaDetails { get; set; }

        /// <summary>
        /// Constructor
        /// When a new pizza is created, it is assigned a guid
        /// </summary>
        public Pizza()
        {
            Id = Guid.NewGuid();
        }

    } // end of class
} // end of namespace
