using Catalog.Api.app.plugins;
using Catalog.Api.app.domain.dtos.requests.images;
using Catalog.Api.app.domain.error;



namespace Catalog.Api.app.application.usecases.images;


public class DeleteImageUsecase {
	private readonly CloudinaryPlugin _cloudinaryPlugin;

	public DeleteImageUsecase(CloudinaryPlugin cloudinaryPlugin){
		_cloudinaryPlugin = cloudinaryPlugin;
	}


	public async Task<bool> Execute(DeleteImageDTO deleteImageDTO ) {
		
		bool isDeleted = await _cloudinaryPlugin.DeleteImageAsync(deleteImageDTO.PublicId);
		if(!isDeleted) throw CustomError.Conflict("Error al eliminar la imagen");

		return isDeleted;
	}
}
