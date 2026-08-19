using Microsoft.AspNetCore.Mvc;
using Catalog.Api.app.application.usecases.images;
using Catalog.Api.app.domain.common;
using Catalog.Api.app.domain.error;
using Catalog.Api.app.domain.dtos.requests.images;
using Catalog.Api.app.domain.dtos.responses.images;


namespace Catalog.Api.app.presentation.controllers;

[ApiController]
[Route("api/v1/images")]
public class ImageController{
	private readonly UploadImageUsecase _uploadImageUsecase;
	private readonly DeleteImageUsecase _deleteImageUsecase;

	public ImageController(
		UploadImageUsecase uploadImageUsecase,
		DeleteImageUsecase deleteImageUsecase
	){
		_uploadImageUsecase = uploadImageUsecase;
		_deleteImageUsecase = deleteImageUsecase;
	}
	
	[HttpPost("category")]
	public async Task<ApiResponse<ImageResponse>> UploadImage([FromForm] UploadImageCategoryDTO uploadImageCategoryDTO){

		string message = uploadImageCategoryDTO.ValidateField();
		if(message != null) throw CustomError.BadRequest(message);

		var response = await _uploadImageUsecase.Execute(uploadImageCategoryDTO);

		return ApiResponse<ImageResponse>.Success(response, "Imagen subida correctamente.");
	}


	[HttpDelete]
	public async Task<ApiResponse<object>> DeleteImage([FromBody] DeleteImageDTO deleteImageDTO) {

		string message = deleteImageDTO.ValidateField();
		if(message != null) throw CustomError.BadRequest(message);

		await _deleteImageUsecase.Execute(deleteImageDTO);

		return ApiResponse<object>.Success(default, $"Imagen eliminada correctamente.");
	}

}
