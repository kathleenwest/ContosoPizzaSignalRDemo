using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController :ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult<List<PizzaOrder>> GetAll()
        {
            return Ok(_orderService.GetAllPizzaOrders());
        }

        [HttpGet("{id}")]
        public ActionResult<PizzaOrder> Get(Guid id)
        {
            // Validate user inputs
            if (id == default)
            {
                return BadRequest();
            }

            // Retrieve the pizza
            PizzaOrder? pizzaOrder = _orderService.Get(id);

            // Validate the pizza exists
            if (pizzaOrder == null)
            {
                return NotFound();
            }

            // Return the pizza
            return Ok(pizzaOrder);
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] PizzaOrder pizzaOrder)
        {
            // Order the Pizza
            _orderService.OrderPizza(pizzaOrder);

            return Ok(pizzaOrder);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(Guid id, JsonPatchDocument<PizzaOrder> pizzaOrderUpdates)
        {
            // Validate the user input
            if (id == default)
            {
                return BadRequest();
            }

            // Retrieve the pizza
            PizzaOrder? existingPizzaOrder = _orderService.Get(id);

            // Validate the pizza exists
            if (existingPizzaOrder is null)
            {
                return NotFound();
            }

            // Apply specific updates only to the pizza
            pizzaOrderUpdates.ApplyTo(existingPizzaOrder);

            // Update the pizza
            _orderService.Update(existingPizzaOrder);

            // Return successful update status
            return NoContent();
        }

    }
}
