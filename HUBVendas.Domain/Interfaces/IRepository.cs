using System.Collections.Generic;
using System.Threading.Tasks;

namespace HUBVendas.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T?> GetById(int id);

        Task<int> Insert(T entity);

        Task<bool> Update(T entity);

        Task<bool> Delete(T entity);
    }
}