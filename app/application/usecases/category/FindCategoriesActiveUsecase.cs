using Catalog.Api.app.domain.repositories;
using Catalog.Api.app.domain.entities;
using Catalog.Api.app.domain.dtos.responses.category;


namespace Catalog.Api.app.application.usecases.category;


public class FindCategoriesActiveUsecase {
	private readonly ICategoryRepository _repository;

	public FindCategoriesActiveUsecase(ICategoryRepository repository){
		_repository = repository;
	}


	public async Task<List<CategoryResponse>> Execute(){
		List<Category> categories = await _repository.FindByActive();
		return categories.Select(c => new CategoryResponse(c)).ToList();
	}
	
}
