using Catalog.Api.app.domain.entities;

namespace Catalog.Api.app.domain.repositories
{

	public interface ICategoryRepository {

		Task<Category?> FindById( Guid id);

		Task<List<Category>> FindByActive();

		Task<List<Category>> Find();

		Task<Category?> FindBySlug( string slug);

		Task<Category?> FindBySlugAndActive( string slug);
		
		Task<Category> Create(Category category);

		Task <Category> UpdateById(Category category);

		Task <Category> DeleteById(Guid id);
	}
}
