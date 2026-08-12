namespace Catalog.Api.app.domain.dtos.responses.product;



public class ProductResponse{
	public Guid Id { get; set; }
	public Guid CategoryId { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Slug { get; set; } = string.Empty;
	public string ShortDescription { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public decimal Price {get; set;}
	public string Sku { get; set; } = string.Empty;
	public bool IsActive { get; set; } 
	public int Stock { get; set; }
	public int ReservedStock { get; set; }

}
