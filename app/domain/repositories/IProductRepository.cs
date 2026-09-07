using Catalog.Api.app.domain.dtos.requests.querys;
using Catalog.Api.app.domain.dtos.responses.pagination;
using Catalog.Api.app.domain.dtos.responses.product;
using Catalog.Api.app.domain.entities;

namespace Catalog.Api.app.domain.repositories;


public interface IProductRepository {

	Task<PaginationResponse<ProductResponse>> Find(QueryParams queryParams);
	Task<List<ProductResponse>> FindActive();
	Task<ProductResponse?> FindById(Guid id);
	Task<ProductResponse?> FindBySlug(string slug);
	Task<ProductResponse?> FindBySlugActive(string slug);
	Task<List<ProductResponse>> FindByCategory(Guid categoryId);
	Task<List<ProductResponse>> FindByCategoryActive(Guid categoryId);


	Task<ProductResponse?> FindBySKu(string sku);

	Task<ProductResponse> Create(Product product, ProductInventory productInventory);
	Task<ProductResponse> Update(Guid id, ProductResponse product, ProductInventory productInventory);
	Task<ProductResponse?> DeleteById(Guid id);
}
