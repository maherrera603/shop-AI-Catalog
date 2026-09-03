using Catalog.Api.app.domain.entities;
using Catalog.Api.app.domain.dtos.responses.category;


namespace Catalog.Api.app.domain.dtos.responses.category;

public class PaginationCategoryResponse{
	public int TotalItems {get; set;}
	public List<CategoryResponse> Categories {get; set; } = [];
}
