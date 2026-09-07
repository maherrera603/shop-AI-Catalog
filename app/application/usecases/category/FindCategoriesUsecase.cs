using Catalog.Api.app.domain.repositories;
using Catalog.Api.app.domain.entities;
using Catalog.Api.app.domain.dtos.requests.category;
using Catalog.Api.app.domain.dtos.responses.category;


namespace Catalog.Api.app.application.usecases.category;


public class FindCategoriesUsecase {
	private readonly ICategoryRepository _repository;

	public FindCategoriesUsecase(ICategoryRepository repository){
		_repository = repository;
	}


	public async Task<PaginationResponse<CategoryResponse>> Execute(CategoryQueryParams queryParams){
		PaginationResponse<CategoryResponse> paginationCategory = await _repository.Find(queryParams);
		return paginationCategory;
	}
	
}
