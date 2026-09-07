using Catalog.Api.app.domain.repositories;
using Catalog.Api.app.domain.dtos.requests.querys;
using Catalog.Api.app.domain.dtos.responses.pagination;
using Catalog.Api.app.domain.dtos.responses.category;


namespace Catalog.Api.app.application.usecases.category;


public class FindCategoriesUsecase {
	private readonly ICategoryRepository _repository;

	public FindCategoriesUsecase(ICategoryRepository repository){
		_repository = repository;
	}


	public async Task<PaginationResponse<CategoryResponse>> Execute(QueryParams queryParams){
		PaginationResponse<CategoryResponse> paginationCategory = await _repository.Find(queryParams);
		return paginationCategory;
	}
	
}
