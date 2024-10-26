using SalesManagement.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Application.IService
{
    public interface IProductService
    {
        Task<ProductDto> AddProductAsync(CreateProductDto productDto);
        Task<bool> UpdateProductAsync(CreateProductDto productDto);
        Task<bool> DeleteProductAsync(int productId);
        Task<bool> ActiveProductAsync(int productId);
        Task<bool> DeleteMultiProductAsync(List<int> ids);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<PaginatedListDto<ProductDto>> GetProductByPageAndSearchAsync(ProdcutSearch prodcutSearch);
    }
}
