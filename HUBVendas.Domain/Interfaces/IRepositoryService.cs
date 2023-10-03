using System.Threading.Tasks;
using HUBVendas.Domain.Entities;

namespace HUBVendas.Domain.Interfaces
{
    public interface IRepositoryService<T> where T : Entity
    {
        Task<T> GetById(int id);

        Task<int> Create(T entity);

        Task<bool> Update(T entity);

        Task<bool> Delete(T entity);
    }
}