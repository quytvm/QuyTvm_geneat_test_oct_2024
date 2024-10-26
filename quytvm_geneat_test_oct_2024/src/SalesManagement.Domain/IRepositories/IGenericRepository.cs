using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Domain.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IReadOnlyList<T>> GetAll();
        Task<T> Add(T entity);
        Task<bool> Exists(int id);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        IQueryable<T> getListByCondition();
        Task<IReadOnlyList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize);
    }
}
