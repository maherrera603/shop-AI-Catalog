using Dapper;
using System.Data;
using Catalog.Api.app.domain.datasources;
using Catalog.Api.app.domain.entities;
using Catalog.Api.app.infrastructure.database;
using Catalog.Api.app.domain.dtos.requests.category;
using Catalog.Api.app.domain.dtos.responses.category;


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
                    description = category.Description,
                    image = category.ImageUrl,
                    imageProviderId = category.ImageProviderId,
                    isActive = category.IsActive
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

        public async Task<PaginationCategoryResponse> Find(CategoryQueryParams queryParams)
        {
            using var connection = _factory.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@Page", queryParams.Page);
            parameters.Add("@PageSize", queryParams.PageSize);
            parameters.Add("@Status", queryParams.isActive);
            parameters.Add("@Search", queryParams.Search);
            

            var result = await connection.QueryMultipleAsync(
                "sp_categories_all",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            var totalItems = await result.ReadSingleAsync<int>();

            var categories = (await result.ReadAsync<Category>()).ToList();


            return new PaginationCategoryResponse{
                TotalItems = totalItems,
                Categories = categories.Select(c => new CategoryResponse(c)).ToList()
            };
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
                    imageUrl = category.ImageUrl,
                    imageProviderId = category.ImageProviderId,
                    isActive = category.IsActive
                },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
