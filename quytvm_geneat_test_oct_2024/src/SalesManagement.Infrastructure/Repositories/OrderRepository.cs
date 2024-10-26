using Microsoft.EntityFrameworkCore;
using SalesManagement.Domain.IRepositories;
using SalesManagement.Domain.Models;
using SalesManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Order> GetLastOrder(string prefix, string dateString)
        { 
            var lastOrder = await _context.Orders
                .Where(o => o.OrderCode.StartsWith($"{prefix}{dateString}"))
                .OrderByDescending(o => o.OrderCode)
                .FirstOrDefaultAsync();
            return lastOrder;
        }

        public async Task<Order> GetOrderDetails(int id)
        {
            var order = await _context.Orders
                .Include(o => o.ProductOrders)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
            return order;
        }
    }
}
