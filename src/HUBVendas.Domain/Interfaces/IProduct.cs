using System.Collections.Generic;
using System.Threading.Tasks;
using HUBVendas.Domain.Entities;

namespace HUBVendas.Domain.Interfaces {
    public interface IProductService : IRepositoryService<Product> {
        Task<List<Product>> GetList();
    }

    public interface IProductRepository : IRepository<Product> { }
}