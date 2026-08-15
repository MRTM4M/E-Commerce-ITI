using E_commerce_iti.Services;
using E_commerce_iti.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_iti.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] OrderCreateViewModel model)
        {
            _orderService.CreateOrder(model);
            return Ok(new { message = "Order created successfully" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id, [FromBody] string status)
        {
            _orderService.UpdateOrderStatus(id, status);
            return Ok(new { message = "Order status updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _orderService.DeleteOrder(id);
            return Ok(new { message = "Order deleted successfully" });
        }
    }
}
