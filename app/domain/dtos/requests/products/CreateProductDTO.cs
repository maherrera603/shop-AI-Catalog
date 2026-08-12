using Catalog.Api.app.application.validations;


namespace Catalog.Api.app.domain.dtos.requests.product;



public class CreateProductDTO{
	public Guid CategoryId { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Slug { get; set; } = string.Empty;
	public string ShortDescription { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public decimal Price {get; set;}
	public string Sku { get; set; } = string.Empty;
	public int Stock { get; set; }



	public string? ValidateFields()
    {
        if(CategoryId == Guid.Empty) return "Debe seleccionar una categoria";

        if(string.IsNullOrWhiteSpace(Name)) return "Ingrese el nombre del producto";

        if(!Validator.ProductName(Name)) return "El nombre solo permite letras, numeros, espacios y guiones";

        if(string.IsNullOrWhiteSpace(Slug)) return "Ingrese el slug del producto";

        if(!Validator.Slug(Slug)) return "El slug no tiene un formato valido";

        if(!string.IsNullOrWhiteSpace(ShortDescription))
        {
            if(!Validator.TextDescription(ShortDescription)) return "Ingrese una descripcion corta valida";
        }

        if(!string.IsNullOrWhiteSpace(Description))
        {
            if(!Validator.TextDescription(Description)) return "Ingrese una descripcion valida";
        }

        if(Price <= 5000) return "El precio debe ser mayor a 5000";

        if(string.IsNullOrWhiteSpace(Sku)) return "Ingrese el sku del producto";

        if(!Validator.Sku(Sku)) return "El SKU solo permite letras, numeros y guiones";

        if(Stock < 0) return "El stock no puede ser negativo";

        return null;
    }
}
