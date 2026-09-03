using Microsoft.AspNetCore.Mvc;
using Catalog.Api.app.application.usecases.category;
using Catalog.Api.app.domain.common;
using Catalog.Api.app.domain.dtos.responses.category;
using Catalog.Api.app.presentation.attributes;
using Catalog.Api.app.domain.dtos.requests.category;
using Catalog.Api.app.domain.error;



namespace Catalog.Api.app.presentation.controllers;

[ApiController]
[Route("api/v1/categories")]
public class CategoryController : ControllerBase {
	private readonly FindCategoriesActiveUsecase _findCategoriesActiveUsecase;
	private readonly FindCategoriesUsecase _findCategoriesUsecase;
	private readonly CreateCategoryUsecase _createCategoryUsecase;
	private readonly FindCategoryByIdUsecase _findCategoryByIdUsecase;
	private readonly FindCategoryBySlugUsecase _findCategoryBySlugUsecase;
	private readonly UpdateCategoryUsecase _updateCategoryUsecase;
	private readonly DeleteCategoryUsecase _deleteCategoryUsecase;


	public CategoryController(
		FindCategoriesActiveUsecase findCategoriesActiveUsecase, 
		FindCategoriesUsecase findCategoriesUsecase,
		CreateCategoryUsecase createCategoryUsecase,
		FindCategoryByIdUsecase findCategoryByIdUsecase,
		FindCategoryBySlugUsecase findCategoryBySlugUsecase,
		UpdateCategoryUsecase updateCategoryUsecase,
		DeleteCategoryUsecase deleteCategoryUsecase
	){
		_findCategoriesActiveUsecase = findCategoriesActiveUsecase;
		_findCategoriesUsecase = findCategoriesUsecase;
		_createCategoryUsecase = createCategoryUsecase;
		_findCategoryByIdUsecase = findCategoryByIdUsecase;
		_findCategoryBySlugUsecase = findCategoryBySlugUsecase;
		_updateCategoryUsecase = updateCategoryUsecase;
		_deleteCategoryUsecase = deleteCategoryUsecase;

	}

	[HttpGet]
	[AllowAnonymous]
	public async Task<ApiResponse<List<CategoryResponse>>> FindCategoriesActive(){
		List<CategoryResponse> categories = await _findCategoriesActiveUsecase.Execute();
		return ApiResponse<List<CategoryResponse>>.Success(categories, "Categorias obtenidas correctamente.");
	}


	[HttpGet("all")]
	public async Task<ApiResponse<PaginationCategoryResponse>> FindCategories([FromQuery] CategoryQueryParams queryParams){
		PaginationCategoryResponse categories = await _findCategoriesUsecase.Execute( queryParams );

		return ApiResponse<PaginationCategoryResponse>.Success(categories, "Categorias obtenidas correctamente.");
	}

	[HttpPost]
	public async Task<ApiResponse<CategoryResponse>> CreateCategory([FromBody] CreateCategoryDTO createCategoryDTO){

		string message = createCategoryDTO.ValidateFields();
		if(message != null) throw CustomError.BadRequest(message);

		var data = await _createCategoryUsecase.Execute(createCategoryDTO);

		return ApiResponse<CategoryResponse>.Created(data, "La categoria ha sido creada correctamente.");
	}


	[HttpGet("{id:guid}")]
	public async Task<ApiResponse<CategoryResponse>> FindCategoryById(Guid id){

		var data  = await _findCategoryByIdUsecase.Execute(id);
		return ApiResponse<CategoryResponse>.Success(data, "Categoria encontrada.");
	}


	[HttpGet("slug/{slug}")]
	[AllowAnonymous]
	public async Task<ApiResponse<CategoryResponse>> FindCategoryBySlug(string slug){

		var data = await _findCategoryBySlugUsecase.Execute(slug);
		return ApiResponse<CategoryResponse>.Success(data, "Categoria encontrada.");
	}


	[HttpPut("{id:guid}")]
	public async Task<ApiResponse<CategoryResponse>> UpdateCategoryById(Guid id, [FromBody] UpdateCategoryDTO updateCategoryDTO){

		string message = updateCategoryDTO.ValidateFields();
		if(message != null) throw CustomError.BadRequest(message);

		CategoryResponse category = await _updateCategoryUsecase.Execute(id, updateCategoryDTO);

		return ApiResponse<CategoryResponse>.Success(category, "La categoria fue actualizada correctamente.");
	}
	

	[HttpDelete("{id:guid}")]
	public async Task<ApiResponse<CategoryResponse>> DeleteCategoryById(Guid id){
		CategoryResponse category = await _deleteCategoryUsecase.Execute(id);
		return ApiResponse<CategoryResponse>.Success(category, "La categoria ha sido eliminada");
	}
}
    

