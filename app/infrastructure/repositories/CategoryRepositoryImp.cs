using Catalog.Api.app.domain.entities;
using Catalog.Api.app.domain.repositories;
using Catalog.Api.app.domain.datasources;

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

        public Task<List<Category>> Find()
        {
            return _datasource.Find();
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
