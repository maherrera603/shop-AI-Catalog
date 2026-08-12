using Catalog.Api.app.application.validations;

namespace Catalog.Api.app.domain.dtos.requests.category;

public class UpdateCategoryDTO {
	public string Name { get; set; } = string.Empty;
	public string Slug { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public bool IsActive { get; set; } = true;


	public string ValidateFields(){

		if(string.IsNullOrEmpty(Name)) return "Ingrese el nombre de la categoria";

		if(!Validator.Text(Name)) return "El campo nombre no debe contener caracteres especiales, ni tabulaciones";

		if(string.IsNullOrEmpty(Slug)) return "Ingrese el slug de la categoria";

		if(!Validator.TextSlug(Slug)) return "El campo slug no debe contener espacios, ni guiones dobles";

		if(string.IsNullOrEmpty(Description)) return "Ingrese la descripcion de la categoria";

		if(!Validator.TextDescription(Description)) return "La descripción de la categoría contiene caracteres no permitidos.";

		return null;
	}
}
