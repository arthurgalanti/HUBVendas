using HUBVendas.Domain.Entities;
using HUBVendas.Domain.Interfaces;

namespace HUBVendas.Service.Services {
    public class CategoryService : ICategoryService {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
            => _categoryRepository = categoryRepository;

        public Task<IEnumerable<Category>> GetList()
            => _categoryRepository.GetAll();

        public Task<Category> GetById(Guid id)
            => _categoryRepository.GetById(id);

        public Task<bool> Create(Category entity)
            => _categoryRepository.Insert(entity);

        public Task<bool> Update(Category entity)
            => _categoryRepository.Update(entity);

        public Task<bool> Delete(Category entity)
            => _categoryRepository.Delete(entity);
    }
}