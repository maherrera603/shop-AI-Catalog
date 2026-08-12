using Catalog.Api.app.domain.repositories;
using Catalog.Api.app.domain.entities;
using Catalog.Api.app.domain.error;
using Catalog.Api.app.domain.dtos.responses.category;


namespace Catalog.Api.app.application.usecases.category;

public class FindCategoryByIdUsecase {
	private readonly ICategoryRepository _categoryRepository;

	public FindCategoryByIdUsecase(ICategoryRepository categoryRepository){
		_categoryRepository = categoryRepository;
	}

	public async Task<CategoryResponse> Execute(Guid id){
		Category? category = await _categoryRepository.FindById(id);
		if(category == null) throw CustomError.NotFound("La category no existe.");

		return new CategoryResponse(category);
	}
}
