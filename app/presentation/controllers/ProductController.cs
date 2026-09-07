using Microsoft.AspNetCore.Mvc;
using Catalog.Api.app.application.usecases.product;
using Catalog.Api.app.domain.common;
using Catalog.Api.app.domain.dtos.requests.querys;
using Catalog.Api.app.domain.dtos.responses.pagination;
using Catalog.Api.app.domain.dtos.responses.product;
using Catalog.Api.app.presentation.attributes;
using Catalog.Api.app.domain.dtos.requests.product;
using Catalog.Api.app.domain.error;


namespace Catalog.Api.app.presentation.controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductController: ControllerBase {
	private readonly FindProductsUsecase _findProductsUsecase;
	private readonly FindProductsByActiveUsecase _findProductsByActiveUsecase;
	private readonly FindProductByIdUsecase _findProductByIdUsecase;
	private readonly FindProductBySlugActiveUsecase _findProductBySlugActiveUsecase;
	private readonly CreateProductUsecase _createProductUsecase;
	private readonly DeleteProductUsecase _deleteProductUsecase;
	private readonly UpdateProductUsecase _updateProductUsecase;

	public ProductController(
		FindProductsUsecase findProductsUsecase,
		FindProductsByActiveUsecase findProductsByActiveUsecase,
		FindProductByIdUsecase findProductByIdUsecase,
		FindProductBySlugActiveUsecase findProductBySlugActiveUsecase,
		CreateProductUsecase createProductUsecase,
		DeleteProductUsecase deleteProductUsecase,
		UpdateProductUsecase updateProductUsecase
	){
		_findProductsUsecase = findProductsUsecase;
		_findProductsByActiveUsecase = findProductsByActiveUsecase;
		_findProductByIdUsecase = findProductByIdUsecase;
		_findProductBySlugActiveUsecase = findProductBySlugActiveUsecase;
		_createProductUsecase = createProductUsecase;
		_deleteProductUsecase = deleteProductUsecase;
		_updateProductUsecase = updateProductUsecase;
	}

	[HttpGet("all")]
	public async Task<ApiResponse<PaginationResponse<ProductResponse>>> Find([FromQuery] QueryParams queryParams){
		var products = await _findProductsUsecase.Execute(queryParams);
		return ApiResponse<PaginationResponse<ProductResponse>>.Success(products, "Listado de productos");
	}

	[HttpGet]
	[AllowAnonymous]
	public async Task<ApiResponse<List<ProductResponse>>> FindByActive(){		
		var products = await _findProductsByActiveUsecase.Execute();
		return ApiResponse<List<ProductResponse>>.Success(products, "Listado de productos");
	}

	[HttpGet("{id:guid}")]
	public async Task<ApiResponse<ProductResponse>> FindById(Guid id){
		var product = await _findProductByIdUsecase.Execute(id);
		return ApiResponse<ProductResponse>.Success(product, "datos del producto");
	}
	

	[HttpGet("slug/{slug}")]
	[AllowAnonymous]
	public async Task<ApiResponse<ProductResponse>> FindBySlug(string slug){
		var response = await _findProductBySlugActiveUsecase.Execute(slug);
		return ApiResponse<ProductResponse>.Success(response, "datos del producto");
	}

	[HttpPost]
	public async Task<ApiResponse<ProductResponse>> Create([FromBody] CreateProductDTO createProductDTO){

		string message = createProductDTO.ValidateFields();
		if(message != null) throw CustomError.BadRequest(message);
		
		var response = await _createProductUsecase.Execute(createProductDTO);

		return ApiResponse<ProductResponse>.Success(response, "Producto creado correctamente");
	}


	[HttpPut("{id:guid}")]
	public async Task<ApiResponse<ProductResponse>> Update(Guid id, [FromBody] UpdateProductDTO updateProductDTO){

		var response = await _updateProductUsecase.Execute(id, updateProductDTO);

		return ApiResponse<ProductResponse>.Success(response, "Producto actualizado correctamente");
	}


	[HttpDelete("{id:guid}")]
	public async Task<ApiResponse<ProductResponse>> Delete(Guid id){

		var response = await _deleteProductUsecase.Execute(id);
		
		return ApiResponse<ProductResponse>.Success(response, "Producto eliminado correctamente");
	}
}
