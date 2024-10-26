using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesManagement.Application.Dto;
using SalesManagement.Application.IService;

namespace SalesManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetProducts() {
            return Ok(await _productService.GetAllProductsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
        {
            return Ok(await _productService.AddProductAsync(productDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] CreateProductDto productDto)
        {
            return Ok(await _productService.UpdateProductAsync(productDto));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok(await _productService.DeleteProductAsync(id));
        }

        [HttpPatch]
        public async Task<IActionResult> ActiveProduct(int id)
        {
            return Ok(await _productService.ActiveProductAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetByPageOrSearch(string? SearchString, string SortOrder = "date_desc",
            int PageNumber = 1, int PageSize = 10)
        {
            var productSearch = new ProdcutSearch()
            {
                searchString = SearchString,
                pageNumber = PageNumber,
                sortOrder = SortOrder,
                pageSize = PageSize
            };
            return Ok(await _productService.GetProductByPageAndSearchAsync(productSearch));
        }

        [HttpDelete("DeleteMultiple")]
        public async Task<IActionResult> DeleteMultipleProducts([FromBody] List<int> productIds)
        {
            if (productIds == null || !productIds.Any())
            {
                return BadRequest("Danh sách sản phẩm cần xóa không được rỗng.");
            }
            var result = await _productService.DeleteMultiProductAsync(productIds);

            return Ok(result);
        }

    }
}
