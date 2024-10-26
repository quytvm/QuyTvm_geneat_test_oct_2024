using SalesManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Domain.IRepositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order> GetOrderDetails(int id);
        Task<Order> GetLastOrder(string prefix, string dateString);
    }
}
