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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteMultiProducts(List<int> ids)
        {
            var products = await _context.Products
                            .Where(p => ids.Contains(p.ProductId))
                            .ToListAsync();

            if (products.Any())
            {
                _context.Products.RemoveRange(products);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Product> GetLastProduct()
        { 
            var lastEntity = await _context.Products.OrderByDescending(p => p.ProductCode).FirstOrDefaultAsync();
            return lastEntity;  
        }

        public async Task<Product> GetProductByCode(string code)
        {
            return await _context.Products.FirstOrDefaultAsync(o => o.ProductCode == code);
        }
    }
}
