using Catalog.Api.app.domain.repositories;
using Catalog.Api.app.domain.entities;
using Catalog.Api.app.domain.error;
using Catalog.Api.app.domain.dtos.responses.category;

namespace Catalog.Api.app.application.usecases.category;

public class FindCategoryBySlugUsecase {
	private readonly ICategoryRepository _categoryRepository;

	public FindCategoryBySlugUsecase(ICategoryRepository categoryRepository){
		_categoryRepository = categoryRepository;
	}

	public async Task<CategoryResponse> Execute(string slug){
		Category? category = await _categoryRepository.FindBySlugAndActive(slug);
		if(category == null) throw CustomError.NotFound("La category no existe.");

		return new CategoryResponse( category );
	}
}
