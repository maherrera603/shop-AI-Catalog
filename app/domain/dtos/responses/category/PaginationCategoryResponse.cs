using Catalog.Api.app.domain.entities;
using Catalog.Api.app.domain.dtos.responses.category;


namespace Catalog.Api.app.domain.dtos.responses.category;

public class PaginationResponse<T>{
	public int TotalItems {get; set;}
	public List<T> Items {get; set; } = [];
}
