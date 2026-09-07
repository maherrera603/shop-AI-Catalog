using Catalog.Api.app.domain.dtos.requests.querys;
using Catalog.Api.app.domain.dtos.responses.category;
using Catalog.Api.app.domain.dtos.responses.pagination;
using Catalog.Api.app.domain.entities;

namespace Catalog.Api.app.domain.datasources
{

	public interface ICategoryDatasource {

		Task<Category?> FindById( Guid id);

		Task<List<Category>> FindByActive();

		Task<PaginationResponse<CategoryResponse>> Find(QueryParams queryParams);

		Task<Category?> FindBySlug( string slug);

		Task<Category?> FindBySlugAndActive( string slug);

		Task<Category> Create(Category category);

		Task <Category> UpdateById(Category category);

		Task <Category> DeleteById(Guid id);
	}
}
