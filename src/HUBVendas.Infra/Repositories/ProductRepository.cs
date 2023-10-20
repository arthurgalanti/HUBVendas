using Dapper;
using HUBVendas.Domain.Entities;
using HUBVendas.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace HUBVendas.Infra.Repositories {
    public class ProductRepository : IProductRepository {
        private readonly string? _connectionString;

        public ProductRepository(IConfiguration configuration) {
            _connectionString = configuration.GetConnectionString("MySQL");
            if (_connectionString == null)
            throw new InvalidDataException("ConnectionString não foi encontrada!");
        }

        public async Task<IEnumerable<Product>> GetAll() {
            IEnumerable<Product> products;

            using (var con = new MySqlConnection(_connectionString)) {
                var query = @" 
                    SELECT
                        *
                    FROM tb_products;
                ";

                var result = await con.QueryAsync<dynamic>(query);

                products = result.Select(item => new Product {
                    Id = item.id,
                    CreatedOn = item.created_on,
                    Active = item.fl_active,
                    Name = item.product_name,
                    Description = item.product_description,
                    Sku = item.sku,
                    BarCode = item.bar_code,
                    CostPrice = item.cost_price,
                    SellingPrice = item.selling_price,
                    Stock = item.stock,
                    Category = new Category {
                        Id = item.category_id
                    },
                    Image = new ProductImage {
                        Name = item.image_name,
                        Type = item.image_type,
                        Base64 = item.image_base64
                    }
                });
            };

            return products;
        }
        public async Task<Product?> GetById(Guid id) {
            Product? product;

            var prm = new DynamicParameters();
            prm.Add("@product_id", id);

            using (var con = new MySqlConnection(_connectionString)) {
                var query = @" 
                    SELECT
                        *
                    FROM tb_products
                    WHERE
                        id = @product_id;
                ";

                var result = await con.QueryAsync<dynamic>(query, prm);

                product = result.Select(item => new Product {
                    Id = item.id,
                    CreatedOn = item.created_on,
                    Active = item.fl_active,
                    Name = item.product_name,
                    Description = item.product_description,
                    Sku = item.sku,
                    BarCode = item.bar_code,
                    CostPrice = item.cost_price,
                    SellingPrice = item.selling_price,
                    Stock = item.stock,
                    Category = new Category {
                        Id = item.category_id
                    },
                    Image = new ProductImage {
                        Name = item.image_name,
                        Type = item.image_type,
                        Base64 = item.image_base64
                    }
                }).FirstOrDefault();
            };
            return product;
        }

        public async Task<bool> Insert(Product entity) {
            using var con = new MySqlConnection(_connectionString);
            try {
                var prm = new DynamicParameters();
                prm.Add("@product_id", entity.Id);
                prm.Add("@created_on", entity.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"));
                prm.Add("@product_name", entity.Name);
                prm.Add("@product_description", entity.Description);
                prm.Add("@sku", entity.Sku);
                prm.Add("@bar_code", entity.BarCode);
                prm.Add("@selling_price", entity.SellingPrice);
                prm.Add("@cost_price", entity.CostPrice);
                prm.Add("@stock", entity.Stock);
                prm.Add("@image_name", entity.Image?.Name);
                prm.Add("@image_type", entity.Image?.Type);
                prm.Add("@image_base64", entity.Image?.Base64);
                prm.Add("@category_id", entity.Category.Id);

                var query = @"
                INSERT INTO tb_products
                (
                    id,
                    created_on,
                    fl_active,
                    product_name,
                    product_description,
                    sku,
                    bar_code,
                    selling_price,
                    cost_price,
                    stock,
                    image_name,
                    image_type,
                    image_base64,
                    category_id
                )
                VALUES (
                    @product_id,
                    @created_on,
                    1,
                    @product_name,
                    @product_description,
                    @sku,
                    @bar_code,
                    @selling_price,
                    @cost_price,
                    @stock,
                    @image_name,
                    @image_type,
                    @image_base64,
                    @category_id
                );
            ";

                var exec = await con.ExecuteAsync(query, prm);

                return exec > 0;
            }
            catch (Exception ex) {
                throw new Exception($"Erro ao inserir produto: {ex.Message}");
            }

        }

        public async Task<bool> Update(Product entity) {
            bool result = false;

            var prm = new DynamicParameters();
            prm.Add("@product_id", entity.Id);
            prm.Add("@fl_active", entity.Active ? 1 : 0);
            prm.Add("@product_name", entity.Name);
            prm.Add("@product_description", entity.Description);
            prm.Add("@sku", entity.Sku);
            prm.Add("@bar_code", entity.BarCode);
            prm.Add("@selling_price", entity.SellingPrice);
            prm.Add("@cost_price", entity.CostPrice);
            prm.Add("@stock", entity.Stock);
            prm.Add("@category_id", entity.Category.Id);
            prm.Add("@image_name", entity.Image?.Name);
            prm.Add("@image_type", entity.Image?.Type);
            prm.Add("@image_base64", entity.Image?.Base64);

            using (var con = new MySqlConnection(_connectionString)) {
                var query = @"
                    UPDATE tb_products
                    SET
                        fl_active = @fl_active,
                        product_name = @product_name,
                        product_description = @product_description,
                        sku = @sku,
                        bar_code = @bar_code,
                        selling_price = @selling_price,
                        cost_price = @cost_price,
                        stock = @stock,
                        category_id = @category_id,
                        image_name = @image_name,
                        image_type = @image_type,
                        image_base64 = @image_base64
                    WHERE
                        id = @product_id;
                ";

                var exec = await con.ExecuteAsync(query, prm);

                result = exec > 0;
            }

            return result;
        }

        public async Task<bool> Delete(Product entity) {
            bool result = false;

            var prm = new DynamicParameters();
            prm.Add("@product_id", entity.Id);

            using (var con = new MySqlConnection(_connectionString)) {
                var query = @"
                    DELETE FROM tb_products
                    WHERE
                        id = @product_id;;
                ";

                var exec = await con.ExecuteAsync(query, prm);

                result = exec > 0;
            }

            return result;
        }
    }
}