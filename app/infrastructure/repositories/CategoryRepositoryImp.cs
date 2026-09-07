using Catalog.Api.app.domain.entities;
using Catalog.Api.app.domain.repositories;
using Catalog.Api.app.domain.datasources;
using Catalog.Api.app.domain.dtos.requests.category;
using Catalog.Api.app.domain.dtos.responses.category;



namespace Catalog.Api.app.infrastructure.repositories{

    public class CategoryRepositoryImp : ICategoryRepository {
        private readonly ICategoryDatasource _datasource;

        public CategoryRepositoryImp(ICategoryDatasource datasource){
            _datasource = datasource;
        }

        public Task<Category> Create(Category category)
        {
            return _datasource.Create(category);
        }

        public Task<Category> DeleteById(Guid id)
        {
            return _datasource.DeleteById(id);
        }

        public Task<PaginationResponse<CategoryResponse>> Find(CategoryQueryParams queryParams)
        {
            return _datasource.Find(queryParams);
        }

        public Task<List<Category>> FindByActive()
        {
            return _datasource.FindByActive();
        }

        public Task<Category?> FindById(Guid id)
        {
            return _datasource.FindById(id);
        }

        public Task<Category?> FindBySlug(string slug)
        {
            return _datasource.FindBySlug(slug);
        }

        public Task<Category?> FindBySlugAndActive(string slug)
        {
            return _datasource.FindBySlugAndActive(slug);
        }

        public Task<Category> UpdateById(Category category)
        {
            return _datasource.UpdateById(category);
        }
    }
}
