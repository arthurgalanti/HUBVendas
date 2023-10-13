using Dapper;
using HUBVendas.Domain.Entities;
using HUBVendas.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace HUBVendas.Infra.Repositories {
    public class ProductRepository : IProductRepository {
        private readonly string? _connectionString;

        public ProductRepository(IConfiguration configuration) {
            _connectionString = configuration?.GetConnectionString("MySQL");
            if (_connectionString == null) {
                throw new InvalidOperationException("A ConnectionString 'MySQL' não foi encontrada na configuração.");
            }
        }

        public async Task<IEnumerable<Product>> GetAll() {
            IEnumerable<Product> products;

            using (var con = new MySqlConnection(_connectionString)) {
                var query = @" 
                    SELECT
                        *
                    FROM tb_product
                    WHERE fl_removed = 0;
                ";

                var result = await con.QueryAsync<dynamic>(query);

                products = result.Select(item => new Product {
                    Id = item.product_id,
                    CreatedOn = item.created_on,
                    Active = item.fl_active,
                    Removed = item.fl_removed,
                    Name = item.product_name,
                    Description = item.product_description,
                    UnitPrice = item.unit_price,
                    Quantity = item.quantity,
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
                    FROM tb_product
                    WHERE
                        product_id = @product_id
                        AND fl_removed = 0;
                ";

                var result = await con.QueryAsync<dynamic>(query, prm);

                product = result.Select(item => new Product {
                    Id = item.product_id,
                    CreatedOn = item.created_on,
                    Active = item.fl_active,
                    Removed = item.fl_removed,
                    Name = item.product_name,
                    Description = item.product_description,
                    UnitPrice = item.unit_price,
                    Quantity = item.quantity,
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
            var result = false;

            var prm = new DynamicParameters();
            prm.Add("@product_id", entity.Id);
            prm.Add("@created_on", entity.CreatedOnString);
            prm.Add("@product_name", entity.Name);
            prm.Add("@product_description", entity.Description);
            prm.Add("@unit_price", entity.UnitPrice);
            prm.Add("@quantity", entity.Quantity);
            prm.Add("@image_name", entity.Image?.Name);
            prm.Add("@image_type", entity.Image?.Type);
            prm.Add("@image_base64", entity.Image?.Base64);
            prm.Add("@category_id", entity.Category.Id);

            using (var con = new MySqlConnection(_connectionString)) {
                var query = @"
                    INSERT INTO tb_product
                    (
						product_id,
                        created_on,
                        fl_active,
                        fl_removed,
                        product_name,
                        product_description,
                        unit_price,
                        quantity,
                        image_name,
                        image_type,
                        image_base64,
                        category_id
                    )
                    VALUES (
						@product_id,
                        @created_on,
                        1,
                        0,
                        @product_name,
                        @product_description,
                        @unit_price,
                        @quantity,
                        @image_name,
                        @image_type,
                        @image_base64,
                        @category_id
                    );
                ";

                var exec = await con.ExecuteAsync(query, prm);

                result = exec > 0;
            };

            return result;
        }

        public async Task<bool> Update(Product entity) {
            bool result = false;

            var prm = new DynamicParameters();
            prm.Add("@product_id", entity.Id);
            prm.Add("@fl_active", entity.Active ? 1 : 0);
            prm.Add("@product_name", entity.Name);
            prm.Add("@product_description", entity.Description);
            prm.Add("@unit_price", entity.UnitPrice);
            prm.Add("@quantity", entity.Quantity);
            prm.Add("@category_id", entity.Category.Id);
            prm.Add("@image_name", entity.Image?.Name);
            prm.Add("@image_type", entity.Image?.Type);
            prm.Add("@image_base64", entity.Image?.Base64);

            using (var con = new MySqlConnection(_connectionString)) {
                var query = @"
                    UPDATE tb_product
                    SET
                        fl_active = @fl_active,
                        product_name = @product_name,
                        product_description = @product_description,
                        unit_price = @unit_price,
                        quantity = @quantity,
                        category_id = @category_id,
                        image_name = @image_name,
                        image_type = @image_type,
                        image_base64 = @image_base64
                    WHERE
                        product_id = @product_id
                        AND fl_removed = 0;
                ";

                var exec = await con.ExecuteAsync(query, prm);

                result = (exec > 0);
            }

            return result;
        }

        public async Task<bool> Delete(Product entity) {
            bool result = false;

            var prm = new DynamicParameters();
            prm.Add("@product_id", entity.Id);

            using (var con = new MySqlConnection(_connectionString)) {
                var query = @"
                    UPDATE tb_product
                    SET
                        fl_removed = 1,
                        fl_active = 0
                    WHERE
                        product_id = @product_id
                        AND fl_removed = 0;
                ";

                var exec = await con.ExecuteAsync(query, prm);

                result = (exec > 0);
            }

            return result;
        }
    }
}