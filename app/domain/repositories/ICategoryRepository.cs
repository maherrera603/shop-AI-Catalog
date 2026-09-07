using Catalog.Api.app.domain.entities;
using Catalog.Api.app.domain.dtos.requests.category;
using Catalog.Api.app.domain.dtos.responses.category;

namespace Catalog.Api.app.domain.repositories
{

	public interface ICategoryRepository {

		Task<Category?> FindById( Guid id);

		Task<List<Category>> FindByActive();

		Task<PaginationResponse<CategoryResponse>> Find(CategoryQueryParams queryParams);

		Task<Category?> FindBySlug( string slug);

		Task<Category?> FindBySlugAndActive( string slug);
		
		Task<Category> Create(Category category);

		Task <Category> UpdateById(Category category);

		Task <Category> DeleteById(Guid id);
	}
}
