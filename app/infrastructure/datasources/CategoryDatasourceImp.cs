using Dapper;
using System.Data;
using Catalog.Api.app.domain.datasources;
using Catalog.Api.app.domain.entities;
using Catalog.Api.app.infrastructure.database;

namespace Catalog.Api.app.infrastructure.datasources
{

    public class CategoryDatasourceImp : ICategoryDatasource {
        private readonly IDBConnectionFactory _factory;

        public CategoryDatasourceImp(IDBConnectionFactory factory){
            _factory = factory;
        }

        public async Task<Category> Create(Category category)
        {
            using var connetion = _factory.CreateConnection();

            return await connetion.QuerySingleAsync<Category>(
                "sp_create_category",
                new {
                    name = category.Name,
                    slug = category.Slug,
                    description = category.Description
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Category> DeleteById(Guid id)
        {
            using var connection = _factory.CreateConnection();

            return await connection.QuerySingleOrDefaultAsync<Category>(
                "sp_delete_categoty_by_id",
                new { id },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<List<Category>> Find()
        {
            using var connection = _factory.CreateConnection();

            var categories = await connection.QueryAsync<Category>(
                "sp_categories_all",
                commandType: CommandType.StoredProcedure
            );

            return categories.ToList();
        }

        public async Task<List<Category>> FindByActive()
        {
            using var connection = _factory.CreateConnection();

            var categories = await connection.QueryAsync<Category>(
                "sp_categories_by_active",
                commandType: CommandType.StoredProcedure
            );

            return categories.ToList();
        }

        public async Task<Category?> FindBySlug(string slug){

            using var connection = _factory.CreateConnection();

            Category? category = await connection.QueryFirstOrDefaultAsync<Category>(
                "sp_category_by_slug",
                new { slug },
                commandType: CommandType.StoredProcedure
            );

            return category;
        }


        public async Task<Category?> FindBySlugAndActive(string slug){

            using var connection = _factory.CreateConnection();

            Category? category = await connection.QueryFirstOrDefaultAsync<Category>(
                "sp_category_by_slug_and_active",
                new { slug },
                commandType: CommandType.StoredProcedure
            );

            return category;
        }

        public async Task<Category?> FindById(Guid id)
        {
            using var connection = _factory.CreateConnection();

            return await connection.QuerySingleOrDefaultAsync<Category?>(
                "sp_category_by_id",
                new { id },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Category> UpdateById(Category category)
        {
            using var connection = _factory.CreateConnection();

            return await connection.QuerySingleAsync<Category>(
                "sp_update_category",
                new {
                    id = category.Id,
                    name = category.Name,
                    slug = category.Slug,
                    description = category.Description,
                    isActive = category.IsActive
                },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
