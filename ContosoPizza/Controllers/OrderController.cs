using ContosoPizza.Filters;
using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [CustomExceptionFilter]
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// Reference to the Order Service
        /// </summary>
        private readonly IOrderService _orderService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="orderService">IOrderService order service reference</param>
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get All Pizzas
        /// </summary>
        /// <returns>List of (PizzaOrder) objects</returns>
        [HttpGet]       
        public ActionResult<List<PizzaOrder>> GetAll()
        {
            return Ok(_orderService.GetAllPizzaOrders());
        }

        /// <summary>
        /// Get Pizza Order (Single)
        /// </summary>
        /// <param name="id">Guid of the pizza order</param>
        /// <returns>single (PizzaOrder)</returns>
        [HttpGet("{id}")]
        public ActionResult<PizzaOrder> Get(Guid id)
        {
            // Validate user inputs
            if (id == default)
            {
                return BadRequest();
            }

            // Retrieve the pizza
            PizzaOrder? pizzaOrder = _orderService.GetPizzaOrder(id);

            // Validate the pizza exists
            if (pizzaOrder == null)
            {
                return NotFound();
            }

            // Return the pizza
            return Ok(pizzaOrder);
        }

        /// <summary>
        /// Create Pizza Order
        /// </summary>
        /// <param name="pizzaOrder">a valid (PizzaOrder)</param>
        /// <returns>Created Pizza Order Guid</returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] PizzaOrder pizzaOrder)
        {
            // Order the Pizza
            await _orderService.OrderPizzaAsync(pizzaOrder);

            // Return the Created Pizza Order
            return CreatedAtAction(nameof(CreateOrderAsync), new {pizzaOrder.OrderId});
        }

        /// <summary>
        /// Patch Update a Pizza Order
        /// </summary>
        /// <param name="id">Guid id of the pizza order</param>
        /// <param name="pizzaOrderUpdates">JsonPatchDocument with Pizza Order updates</param>
        /// 
        /// JSON Example: 
        /// [
        ///   {
        ///     "value": "Preparation",
        ///     "path": "/status",
        ///     "op": "replace"
        ///   }
        /// ] 
        /// <returns></returns>
        [HttpOptions]
        [HttpPatch("{id}")]
        public IActionResult Patch(Guid id, JsonPatchDocument<PizzaOrder> pizzaOrderUpdates)
        {
            // Validate the user input
            if (id == default)
            {
                return BadRequest();
            }

            // Retrieve the pizza
            PizzaOrder? existingPizzaOrder = _orderService.GetPizzaOrder(id);

            // Validate the pizza exists
            if (existingPizzaOrder is null)
            {
                return NotFound();
            }

            // Apply specific patch updates only to the existing pizza order
            pizzaOrderUpdates.ApplyTo(existingPizzaOrder);

            // Update the existing pizza with the patch updates
            _orderService.UpdatePizzaOrder(existingPizzaOrder);

            // Return successful update status
            return NoContent();
        }

    } // end of class
} // end of namespace
