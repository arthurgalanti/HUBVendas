using Dapper;
using HUBVendas.Domain.Entities;
using HUBVendas.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace HUBVendas.Infra.Repositories {
    public class CategoryRepository : ICategoryRepository {
        private readonly string? _connectionString;

        public CategoryRepository(IConfiguration configuration) {
            _connectionString = configuration.GetConnectionString("MySQL");
        }

        public async Task<IEnumerable<Category>> GetAll() {
            IEnumerable<Category> categories;

            using (var con = new MySqlConnection(_connectionString)) {
                var query = @" 
                    SELECT 
                    * 
                    FROM tb_categories;
                ";

                var result = await con.QueryAsync<dynamic>(query);

                categories = result.Select(item => new Category {
                    Id = item.id,
                    CreatedOn = item.created_on,
                    Active = item.fl_active,
                    Name = item.category_name,
                    Description = item.category_description
                });
            };

            return categories;
        }

        public async Task<Category?> GetById(Guid id) {
            Category? category;

            var prm = new DynamicParameters();
            prm.Add("@category_id", id);

            using (var con = new MySqlConnection(_connectionString)) {
                var query = @"
                    SELECT
                        *
                    FROM tb_categories
                    WHERE
                        id = @category_id;
                ";

                var result = await con.QueryAsync<dynamic>(query, prm);

                category = result.Select(item => new Category {
                    Id = id,
                    CreatedOn = item.created_on,
                    Active = item.fl_active,
                    Name = item.category_name,
                    Description = item.category_description
                }).FirstOrDefault();
            };

            return category;
        }

        public async Task<bool> Insert(Category entity) {
            var result = false;

            var prm = new DynamicParameters();
            prm.Add("@category_id", entity.Id);
            prm.Add("@category_name", entity.Name);
            prm.Add("@category_description", entity.Description);
            prm.Add("@created_on", entity.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"));

            using (var con = new MySqlConnection(_connectionString)) {
                var query = @"
                    INSERT INTO tb_categories (id, created_on, fl_active, category_name, category_description)
                    VALUES (
						@category_id,
                        @created_on,
                        1,
                        @category_name,
                        @category_description
                    );
                ";

                var exec = await con.ExecuteAsync(query, prm);

                result = exec > 0;
            };

            return result;
        }

        public async Task<bool> Update(Category entity) {
            bool result = false;

            var prm = new DynamicParameters();
            prm.Add("@category_id", entity.Id);
            prm.Add("@category_name", entity.Name);
            prm.Add("@category_description", entity.Description);
            prm.Add("@fl_active", entity.Active ? 1 : 0);

            using (var con = new MySqlConnection(_connectionString)) {
                var query = @"
                    UPDATE tb_categories
                    SET
                        fl_active = @fl_active,
                        category_name = @category_name,
                        category_description = @category_description
                    WHERE
                        id = @category_id;
                ";

                var exec = await con.ExecuteAsync(query, prm);

                result = exec > 0;
            }

            return result;
        }
        public async Task<bool> Delete(Category entity) {
            bool result = false;

            var prm = new DynamicParameters();
            prm.Add("@category_id", entity.Id);

            using (var con = new MySqlConnection(_connectionString)) {
                var query = @"
                    DELETE FROM tb_categories
                    WHERE
                        id = @category_id;
                ";

                var exec = await con.ExecuteAsync(query, prm);

                result = exec > 0;
            }

            return result;
        }
    }
}