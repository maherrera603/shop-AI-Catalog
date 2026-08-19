namespace Catalog.Api.app.domain.dtos.requests.images;

public class UploadImageCategoryDTO{
	public IFormFile? Image {get; set;}
	private string[] _extensions = [".webp", ".png"];

	public string? ValidateField(){
		if(Image == null) return "Seleccione la imagen correspondiente";
		

		if(!_extensions.Contains(Path.GetExtension(Image.FileName))){ 
			var extensionsPermited = string.Join(", ", _extensions);
			return $"Solo se permite archivos con las extensiones: {extensionsPermited}";
		}

		return null;
	}
}
