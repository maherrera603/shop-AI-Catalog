using Catalog.Api.app.domain.dtos.requests.images;
using Catalog.Api.app.domain.dtos.responses.images;
using Catalog.Api.app.plugins;


namespace Catalog.Api.app.application.usecases.images;

public class UploadImageUsecase{
	private readonly CloudinaryPlugin _cloudinaryPlugin;

	public UploadImageUsecase(CloudinaryPlugin cloudinaryPlugin){
		_cloudinaryPlugin = cloudinaryPlugin;
	}


	public async Task<ImageResponse> Execute(UploadImageCategoryDTO uploadImageCategoryDTO){
		
		using var streamImage = uploadImageCategoryDTO.Image!.OpenReadStream();
		var imageResponse = await _cloudinaryPlugin.UploadImageAsync(streamImage, uploadImageCategoryDTO.Image.FileName, "shopai/categories");

		return imageResponse;
	}
}
