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

        public async Task<Order> GetLastOrder()
        {
            var lastEntity = await _context.Orders.OrderByDescending(p => p.OrderCode).FirstOrDefaultAsync();
            return lastEntity;
        }

        public async Task<Order> GetOrderDetails(int id)
        {
            var order = await _context.Orders
                .Include(o => o.ProductOrders)
                .FirstOrDefaultAsync(p => p.Id == id);
            return order;
        }
    }
}
