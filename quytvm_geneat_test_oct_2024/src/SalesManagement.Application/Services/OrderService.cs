using AutoMapper;
using SalesManagement.Application.Dto;
using SalesManagement.Application.Exceptions;
using SalesManagement.Application.IServices;
using SalesManagement.Domain.IRepositories;
using SalesManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IProductRepository productRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddOrderAsync(CreateOrderDto orderDto)
        {
            foreach (var productOrderDto in orderDto.productOrders)
            {
                if (! await _productRepository.Exists(productOrderDto.ProductId))
                {
                    throw new FoundNotProducException($"Product with ID {productOrderDto.ProductId} does not exist.");
                }
            }

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var order = new Order()
                {
                    CustomerName = orderDto.CustomerName,
                    CustomerPhone = orderDto.CustomerPhone,
                    OrderCode = await GenerateOrderCode(),
                    TotalAmount = 0,
                    ProductOrders = new List<ProductOrder>(),
                    TotalTax = 0,
                };

                foreach (var productOrderDto in orderDto.productOrders)
                {
                    Console.WriteLine(productOrderDto.ProductId);
                    var productOrder = new ProductOrder()
                    {
                        ProductId = productOrderDto.ProductId,
                        Quantity = productOrderDto.Quantity,
                        TaxRate = productOrderDto.TaxRate,
                        UnitPrice = productOrderDto.UnitPrice > 0 ? productOrderDto.UnitPrice : productOrderDto.Product.SalePrice
                    };

                    decimal tax = productOrder.UnitPrice * productOrder.Quantity;
                    decimal taxRate = tax * productOrder.TaxRate / 100;
                    order.TotalTax += productOrder.TaxAmount;
                    order.TotalAmount += productOrder.LineTotal;

                    order.ProductOrders.Add(productOrder);
                }

                await _unitOfWork.Orders.Add(order);

                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _unitOfWork.Orders.GetAll();
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetOrderDtoDetails(int id)
        {
            var orderDetails = await _unitOfWork.Orders.GetOrderDetails(id);
            return _mapper.Map<OrderDto>(orderDetails);
        }

        public async Task<bool> UpdateOrderAsync(CreateOrderDto orderDto)
        {
            var order = await _unitOfWork.Orders.GetOrderDetails(orderDto.Id);

            foreach (var productOrderDto in orderDto.productOrders)
            {
                if (!await _productRepository.Exists(productOrderDto.ProductId))
                {
                    throw new InvalidOperationException($"Product with ID {productOrderDto.ProductId} does not exist.");
                }
            }

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                order.CustomerName = orderDto.CustomerName;
                order.CustomerPhone = orderDto.CustomerPhone;
                order.ProductOrders = new List<ProductOrder>();
                order.TotalAmount = 0;
                order.TotalTax = 0;
                foreach (var productOrderDto in orderDto.productOrders)
                {
                    var productOrder = new ProductOrder()
                    {
                        Id = productOrderDto.Id,
                        OrderId = orderDto.Id,
                        ProductId = productOrderDto.ProductId,
                        Quantity = productOrderDto.Quantity,
                        UnitPrice = productOrderDto.UnitPrice > 0 ? productOrderDto.UnitPrice : productOrderDto.Product.SalePrice,
                        TaxRate = productOrderDto.TaxRate,
                    };

                    decimal tax = productOrder.UnitPrice * productOrder.Quantity;
                    decimal taxRate = tax * productOrder.TaxRate / 100;
                    order.TotalTax += taxRate;
                    order.TotalAmount += tax - taxRate;

                    order.ProductOrders.Add(productOrder);
                }

                await _unitOfWork.Orders.Update(order);

                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }


        private async Task<string> GenerateOrderCode()
        {
            var today = DateTime.Now;
            string prefix = "OD";
            string dateString = today.ToString("ddMMyy");

            var lastOrder = await _unitOfWork.Orders.GetLastOrder(prefix,dateString);

            int sequence = 1;

            if (lastOrder != null)
            {
                string lastSequence = lastOrder.OrderCode.Substring(lastOrder.OrderCode.Length - 3);
                sequence = int.Parse(lastSequence) + 1;
            }

            string newOrderCode = $"{prefix}{dateString}{sequence:D3}";

            return newOrderCode;
        }
    }
}
