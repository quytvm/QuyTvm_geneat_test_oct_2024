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
    public class ProductOrderRepository : GenericRepository<ProductOrder>, IProductOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductOrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
