namespace Catalog.Api.app.domain.entities;

public class ProductInventory {
	public Guid ProductId { get; set; }
	public int Stock { get; set; }
	public int ReservedStock { get; set; }
}
