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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _productService.AddProductAsync(productDto);

            if (result == null)
            {
                return UnprocessableEntity("Failed to create the product.");
            }

            return CreatedAtAction(nameof(GetProducts), new { id = result.ProductId }, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] CreateProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isSuccess = await _productService.UpdateProductAsync(productDto);

            if (!isSuccess)
            {
                return NotFound("Product not found or update failed.");
            }

            return Ok("Product updated successfully.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var isSuccess = await _productService.DeleteProductAsync(id);

            if (!isSuccess)
            {
                return NotFound("Product not found or delete failed.");
            }

            return Ok("Product deleted successfully.");
        }

        [HttpPatch("{id}/Activate")]
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
                return BadRequest("Product ID list cannot be empty.");
            }

            var result = await _productService.DeleteMultiProductAsync(productIds);
            if (!result)
            {
                return StatusCode(500, "An error occurred while deleting multiple products.");
            }
            return Ok("Products deleted successfully.");
        }

    }
}
