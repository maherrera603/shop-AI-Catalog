using Catalog.Api.app.domain.repositories;
using Catalog.Api.app.domain.entities;
using Catalog.Api.app.domain.dtos.requests.category;
using Catalog.Api.app.domain.error;
using Catalog.Api.app.domain.dtos.responses.category;




namespace Catalog.Api.app.application.usecases.category;

public class UpdateCategoryUsecase {
	private readonly ICategoryRepository _categoryRepository;

	public UpdateCategoryUsecase(ICategoryRepository categoryRepository){
		_categoryRepository = categoryRepository;
	}


	public async Task<CategoryResponse> Execute(Guid id, UpdateCategoryDTO updateCategoryDTO){
		Category? category = await _categoryRepository.FindById(id);
		if(category == null) throw CustomError.NotFound("la categoria no fue excontrada");

		if(category.Slug != updateCategoryDTO.Slug){
			Category? slugCategory = await _categoryRepository.FindBySlug(updateCategoryDTO.Slug);
			if(slugCategory != null && slugCategory.Id != category.Id) throw CustomError.Conflict("Ya existe una categoria con ese slug");
		}

		category.Name = updateCategoryDTO.Name;
		category.Slug = updateCategoryDTO.Slug;
		category.Description = updateCategoryDTO.Description;
		category.ImageUrl = updateCategoryDTO.ImageUrl;
		category.ImageProviderId = updateCategoryDTO.ImageProviderId;
		category.IsActive = updateCategoryDTO.IsActive;

		var categoryUpdated = await _categoryRepository.UpdateById(category);

		return new CategoryResponse(categoryUpdated);
	}
}
