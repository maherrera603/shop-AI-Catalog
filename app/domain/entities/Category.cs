using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Catalog.Api.app.domain.entities;


[ Table("categories") ]
public class Category {

	[Key]
	public Guid Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public string Slug { get; set; } = string.Empty;

	public string Description { get; set; } = string.Empty;

	[ Column("image_url")]
	public string ImageUrl { get; set; } = string.Empty;
	
	[ Column("is_active")]
	public bool IsActive { get; set; }

	[ Column("created_at")]
	[ JsonIgnore ]
	public DateTime CreatedAt { get; set; }

	[ Column("updated_at") ]
	[ JsonIgnore ]
	public DateTime UpdatedAt { get; set; }


	public Category(){}


	public Category(string name, string slug, string description){
		Name = name;
		Slug = slug;
		Description = description;
	}
}
