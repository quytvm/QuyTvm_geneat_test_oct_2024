using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesManagement.Application.Dto;
using SalesManagement.Application.IService;
using SalesManagement.Domain.IRepositories;
using SalesManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<bool> ActiveProductAsync(int productId)
        {
            var product = await _productRepository.Get(productId);
            if (product == null) { 
                return false;
            }
            product.UpdatedDate = DateTime.Now;
            product.IsActive = !product.IsActive;
            return await _productRepository.Update(product);
        }

        public async Task<ProductDto> AddProductAsync(CreateProductDto productDto)
        {

            var product = _mapper.Map<Product>(productDto);
            product.ProductCode = await GenerateProductCode();
            var productAdd = await _productRepository.Add(product);
            return _mapper.Map<ProductDto>(productAdd);
        }

        public async Task<bool> DeleteMultiProductAsync(List<int> ids)
        {
            return await _productRepository.DeleteMultiProducts(ids);
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _productRepository.Get(productId);
            return await _productRepository.Delete(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAll();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<PaginatedListDto<ProductDto>> GetProductByPageAndSearchAsync(ProdcutSearch prodcutSearch)
        {
            var products = _productRepository.getListByCondition();
            if (!string.IsNullOrEmpty(prodcutSearch.searchString))
            {
                products = products.Where(s => s.ProductCode.Contains(prodcutSearch.searchString) || s.ProductName.Contains(prodcutSearch.searchString));
            }

            switch (prodcutSearch.sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(e => e.ProductName);
                    break;

                case "date_asc":
                    products = products.OrderBy(e => e.CreatedDate);
                    break;
                case "date_desc":
                    products = products.OrderByDescending(s => s.CreatedDate);
                    break;
            }
            var item1 = await products.ToListAsync();
            if (prodcutSearch.pageNumber < 1)
            {
                prodcutSearch.pageNumber = 1;
            }
            
            var items = await _productRepository.CreateAsync(products, prodcutSearch.pageNumber, prodcutSearch.pageSize);
            var itemsDto = _mapper.Map<List<ProductDto>>(items);
            var pagined = new PaginatedListDto<ProductDto>(itemsDto, item1.Count, prodcutSearch.pageNumber, prodcutSearch.pageSize);
            return pagined;
        }

        public async Task<bool> UpdateProductAsync(CreateProductDto productDto)
        {
            var product = await _productRepository.Get(productDto.ProductId);
            _mapper.Map(productDto, product);
            product.UpdatedDate = DateTime.UtcNow;
            return await _productRepository.Update(product);
        }


        private async Task<string> GenerateProductCode()
        {
            var lastProduct = await _productRepository.GetLastProduct();

            int lastNumber = 0;
            if (lastProduct != null && !string.IsNullOrEmpty(lastProduct.ProductCode))
            {
                var numberPart = lastProduct.ProductCode.Substring(2);
                int.TryParse(numberPart, out lastNumber);
            }
            else
            {
                return "PR0001";
            }

            return $"PR{(lastNumber + 1).ToString("D4")}";
        }
    }
}
