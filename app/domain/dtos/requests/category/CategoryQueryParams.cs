namespace Catalog.Api.app.domain.dtos.requests.category;

public class CategoryQueryParams {
	public int Page { get; set; } = 1;
	public int PageSize {get; set;} = 30;
	public string? Search { get; set;} = string.Empty;
	public string? SortBy { get; set; } = "name";
	public string? SortOrder {get; set;} = "asc";
	public string? isActive {get; set;} = "all";
}
