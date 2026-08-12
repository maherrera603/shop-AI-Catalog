using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Catalog.Api.app.domain.entities;


[ Table("products")]
public class Product {
	[Key]
	public Guid Id { get; set; }

	public Guid CategoryId { get; set; }

	public string Name { get; set; } = string.Empty;

	public string Slug { get; set; } = string.Empty;

	public string ShortDescription { get; set;} = string.Empty;

	public string Description { get; set; } = string.Empty;

	public decimal Price { get; set; }

	public string Sku { get; set; } = string.Empty;

	public bool IsActive { get; set; } = true;
	
	[JsonIgnore]
	public DateTime CreatedAt { get; set; }

	[JsonIgnore]
	public DateTime UpdatedAt { get; set; }


	public Product(){}

}

