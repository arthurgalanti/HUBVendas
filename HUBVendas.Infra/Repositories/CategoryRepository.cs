using Dapper;
using System.Data.SqlClient;
using HUBVendas.Domain.Entities;
using HUBVendas.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace HUBVendas.Infra.Repositories {
    public class CategoryRepository : ICategoryRepository {
        private readonly string? _connectionString;

        public CategoryRepository(IConfiguration configuration) {
            _connectionString = configuration?.GetConnectionString("MySQL");
            if (_connectionString == null) {
                throw new InvalidOperationException("A ConnectionString 'MySQL' não foi encontrada na configuração.");
            }
        }

        public async Task<IEnumerable<Category>> GetAll() {
            IEnumerable<Category> categories;

            using (var con = new SqlConnection(_connectionString)) {
                var query = @" 
                    SELECT 
                    * 
                    FROM tb_category 
                    WHERE fl_removed = 0;
                ";

                var result = await con.QueryAsync<dynamic>(query);

                categories = result.Select(item => new Category {
                    Id = item.category_id,
                    CreatedOn = item.created_on,
                    Active = item.fl_active,
                    Removed = item.fl_removed,
                    Name = item.category_name,
                    Description = item.category_description
                });
            };

            return categories;
        }

        public async Task<Category> GetById(Guid id) {
            Category category;

            var prm = new DynamicParameters();
            prm.Add("@category_id", id);

            using (var con = new SqlConnection(_connectionString)) {
                var query = @"
                    SELECT
                        *
                    FROM tb_category
                    WHERE
                        category_id = @category_id
                        AND fl_removed = 0;
                ";

                var result = await con.QueryAsync<dynamic>(query, prm);

                category = result.Select(item => new Category {
                    Id = item.category_id,
                    CreatedOn = item.created_on,
                    Active = item.fl_active,
                    Removed = item.fl_removed,
                    Name = item.category_name,
                    Description = item.category_description
                }).First();
            };

            return category;
        }

        public async Task<bool> Insert(Category entity) {
            var result = false;

            var prm = new DynamicParameters();
            prm.Add("@category_id", entity.Id);
            prm.Add("@category_name", entity.Name);
            prm.Add("@category_description", entity.Description);
            prm.Add("@created_on", entity.CreatedOn);

            using (var con = new SqlConnection(_connectionString)) {
                var query = @"
                    INSERT INTO tb_category (category_id, created_on, fl_active, fl_removed, category_name, category_description)
                    VALUES (
						@category_id
                        @created_on,
                        1,
                        0,
                        category_name,
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

            using (var con = new SqlConnection(_connectionString)) {
                var query = @"
                    UPDATE tb_category
                    SET
                        fl_active = @fl_active,
                        category_name = @category_name,
                        category_description = @category_description
                    WHERE
                        category_id = @category_id
                        AND fl_removed = 0;
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

            using (var con = new SqlConnection(_connectionString)) {
                var query = @"
                    UPDATE tb_category
                    SET
                        fl_removed = 1,
                        fl_active = 0
                    WHERE
                        category_id = @category_id
                        AND fl_removed = 0;
                ";

                var exec = await con.ExecuteAsync(query, prm);

                result = (exec > 0);
            }

            return result;
        }
    }
}