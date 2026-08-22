using Catalog.Api.app.domain.repositories;
using Catalog.Api.app.domain.entities;
using Catalog.Api.app.domain.dtos.requests.category;
using Catalog.Api.app.domain.dtos.responses.category;
using Catalog.Api.app.domain.error;



namespace Catalog.Api.app.application.usecases.category;

public class CreateCategoryUsecase {
	private readonly ICategoryRepository _categoryRepository;

	public CreateCategoryUsecase(ICategoryRepository categoryRepository){
		_categoryRepository = categoryRepository;
	}

	public async Task<CategoryResponse> Execute(CreateCategoryDTO createCategoryDTO){
		Category? existingCategory = await _categoryRepository.FindBySlug(createCategoryDTO.Slug);
		if(existingCategory != null) throw CustomError.Conflict("Ya existe una categoria con ese slug");

		Category newCategory = new Category{
			Name = createCategoryDTO.Name,
			Slug = createCategoryDTO.Slug,
			Description = createCategoryDTO.Description,
			ImageUrl = createCategoryDTO.ImageUrl,
			ImageProviderId = createCategoryDTO.ImageProviderId,
			IsActive = createCategoryDTO.IsActive
		};

		Category createdCategory = await _categoryRepository.Create(newCategory);

		return new CategoryResponse(createdCategory);
	}

}
