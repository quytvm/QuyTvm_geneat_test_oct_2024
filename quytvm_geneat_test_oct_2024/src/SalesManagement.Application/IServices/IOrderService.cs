using SalesManagement.Application.Dto;
using SalesManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Application.IServices
{
    public interface IOrderService
    {
        Task<bool> AddOrderAsync(CreateOrderDto orderDto);
        Task<bool> UpdateOrderAsync(CreateOrderDto orderDto);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderDtoDetails(int id);
    }
}
