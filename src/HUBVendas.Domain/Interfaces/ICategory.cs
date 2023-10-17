using HUBVendas.Domain.Entities;

namespace HUBVendas.Domain.Interfaces {
    public interface ICategoryService : IRepositoryService<Category> {
        Task<IEnumerable<Category>> GetList();
    }

    public interface ICategoryRepository : IRepository<Category> { }
}