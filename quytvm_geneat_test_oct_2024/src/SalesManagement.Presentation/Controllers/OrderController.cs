using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesManagement.Application.Dto;
using SalesManagement.Application.IServices;

namespace SalesManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _orderService.GetAllOrdersAsync());
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetOrderDetail(int id)
        {
            return Ok(await _orderService.GetOrderDtoDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateOrderDto orderDto)
        {
            return Ok(await _orderService.AddOrderAsync(orderDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] CreateOrderDto orderDto)
        {
            return Ok(await _orderService.UpdateOrderAsync(orderDto));
        }
    }
}
