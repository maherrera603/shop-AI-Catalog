namespace Catalog.Api.app.domain.dtos.requests.images;

public class DeleteImageDTO{
	public string PublicId {get; set; } = string.Empty;


	public string ValidateField(){
		if(string.IsNullOrEmpty(PublicId)) return "Ingrese el publicId de la imagen a eliminar";

		return null;
	}
}
