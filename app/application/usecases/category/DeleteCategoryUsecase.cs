using Catalog.Api.app.domain.entities;
using Catalog.Api.app.domain.error;
using Catalog.Api.app.domain.repositories;
using Catalog.Api.app.domain.dtos.responses.category;

namespace Catalog.Api.app.application.usecases.category;


public class DeleteCategoryUsecase {
	private readonly ICategoryRepository _categoryRepository;

	public DeleteCategoryUsecase(ICategoryRepository categoryRepository){
		_categoryRepository = categoryRepository;
	}

	public async Task<CategoryResponse> Execute(Guid id){
		Category? existCategory = await _categoryRepository.FindById(id);
		if(existCategory == null) throw CustomError.NotFound("La category no fue encontrada");

		Category category = await _categoryRepository.DeleteById(existCategory.Id);

		return new CategoryResponse(category);
	}
}
