using API.Data.Models;
using API.Data.Models.DTOs.Order;
using API.Repository;
using API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        // api/orders/1
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderPostDto orderPostDto)
        {
            var isAdded = await orderRepository.PlaceOrder(orderPostDto);

            if (isAdded)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return BadRequest("Order could not be placed");
            }

        }

        // api/orders/user/1
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUser(int userId)
        {
            var orders = await orderRepository.GetOrdersByUser(userId);
            if (orders.Any())
            {
                return Ok(orders);
            }
            return NotFound("No orders found");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await orderRepository.GetAllOrders();
            if (orders.Any())
            {
                return Ok(orders);
            }
            return NotFound("No orders found");
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrdersDetail(int orderId)
        {
            var orderDetail = await orderRepository.GetOrderDetail(orderId);
            if (orderDetail.Any())
            {
                return Ok(orderDetail);
            }
            return NotFound("No orders found");
        }
    }
}
