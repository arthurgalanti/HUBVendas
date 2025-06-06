using HUBVendas.Domain.Entities;
using HUBVendas.Domain.Interfaces;

namespace HUBVendas.Service.Services {
    public class ProductService : IProductService {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryService _categoryService;

        public ProductService(IProductRepository productRepository, ICategoryService categoryService) {
            _productRepository = productRepository;
            _categoryService = categoryService;
        }
        public async Task<List<Product>> GetList() {
            var products = await _productRepository.GetAll();
            Category? category;
            var result = products.ToList();

            foreach (var p in result) {
                category = await _categoryService.GetById(p.Category!.Id);
                if (category != null)
                    p.Category = category;
            }
            return result;
        }
        public async Task<bool> Create(Product entity)
            => await _productRepository.Insert(entity);

        public async Task<bool> Delete(Product entity)
            => await _productRepository.Delete(entity);

        public async Task<Product?> GetById(Guid id) {
            var product = await _productRepository.GetById(id);
            if (product != null) {
                var category = await _categoryService.GetById(id);
                if (category != null) {
                    product.Category = category;
                }
            }
            return product;
        }

        public async Task<bool> Update(Product entity)
            => await _productRepository.Update(entity);
    }
}