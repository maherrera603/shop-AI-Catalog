
namespace Catalog.Api.app.domain.dtos.responses.pagination;

public class PaginationResponse<T>{
	public int TotalItems {get; set;}
	public List<T> Items {get; set; } = [];
}
