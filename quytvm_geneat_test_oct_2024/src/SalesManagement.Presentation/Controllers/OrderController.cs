using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesManagement.Application.Dto;
using SalesManagement.Application.Exceptions;
using SalesManagement.Application.IServices;

namespace SalesManagement.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            var orderDetail = await _orderService.GetOrderDtoDetails(id);

            if (orderDetail == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }

            return Ok(orderDetail);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
        {
            try
            {
                var result = await _orderService.AddOrderAsync(orderDto);
                return Created(string.Empty, "Order created successfully.");
            }
            catch (FoundNotProducException ex)
            {
                return StatusCode(403, new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] CreateOrderDto orderDto)
        {
            try
            {
                var result = await _orderService.UpdateOrderAsync(orderDto);
                return Ok(new { Message = "Update successful!" });
            }
            catch (FoundNotProducException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
