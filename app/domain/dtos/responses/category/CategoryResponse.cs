using Catalog.Api.app.domain.entities;

namespace Catalog.Api.app.domain.dtos.responses.category;

public class CategoryResponse {


	public Guid Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public string Slug { get; set; } = string.Empty;

	public string Description { get; set; } = string.Empty;

	public string ImageUrl { get; set; } = string.Empty;
	
	public bool IsActive { get; set; }


	public CategoryResponse(Category category){
		Id = category.Id;
		Name = category.Name;
		Slug = category.Slug;
		Description = category.Description;
		ImageUrl = category.ImageUrl;
		IsActive = category.IsActive;
	}
}
