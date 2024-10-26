using Microsoft.EntityFrameworkCore.Storage;
using SalesManagement.Application.IServices;
using SalesManagement.Domain.IRepositories;
using SalesManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction _transaction;
        public IOrderRepository Orders { get; }
        public IProductOrderRepository ProductOrders { get; }


        public UnitOfWork(ApplicationDbContext context, IOrderRepository orderRepository, IProductOrderRepository productOrderRepository)
        {
            _context = context;
            Orders = orderRepository;
            ProductOrders = productOrderRepository;
        }

        public void Dispose()
        {
            _context.Dispose();
            _transaction?.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }
    }
}
