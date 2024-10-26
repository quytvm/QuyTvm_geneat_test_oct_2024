using SalesManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Domain.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetLastProduct();
        Task<Product> GetProductByCode(string code);
        Task<bool> DeleteMultiProducts(List<int> ids);
    }
}
