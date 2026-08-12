using Catalog.Api.app.domain.repositories;
using Catalog.Api.app.domain.dtos.requests.product;
using Catalog.Api.app.domain.dtos.responses.product;
using Catalog.Api.app.domain.entities;
using Catalog.Api.app.domain.error;


namespace Catalog.Api.app.application.usecases.product;

public class CreateProductUsecase {
	private readonly IProductRepository _productRepository;
	private readonly ICategoryRepository _categoryRepository;

	public CreateProductUsecase(IProductRepository productRepository, ICategoryRepository categoryRepository){
		_productRepository = productRepository;
		_categoryRepository = categoryRepository;
	}


	public async Task<ProductResponse> Execute(CreateProductDTO createProductDTO){
		var existProductBySlug = await _productRepository.FindBySlug(createProductDTO.Slug);
		if(existProductBySlug != null) throw CustomError.Conflict($"El producto con Slug {createProductDTO.Slug} ya se encuentra registrado");

		var existProductBySku = await _productRepository.FindBySKu(createProductDTO.Sku);
		if(existProductBySku != null) throw CustomError.Conflict($"El producto con Sku { createProductDTO.Sku} ya se encuentra registrado");

		// validacion si la categoria existe
		var existsCategory = await _categoryRepository.FindById(createProductDTO.CategoryId);
		if(existsCategory == null ) throw CustomError.NotFound("La categoria asignada al producto no existe");

		// creamos el producto
		var product = new Product {
			CategoryId = createProductDTO.CategoryId,
			Name = createProductDTO.Name,
			ShortDescription = createProductDTO.ShortDescription,
			Description = createProductDTO.Description,
			Slug = createProductDTO.Slug,
			Sku = createProductDTO.Sku,
			Price = createProductDTO.Price,
		};

		var productInventory = new ProductInventory{
			Stock = createProductDTO.Stock,
			ReservedStock = 0
		};

		return await _productRepository.Create(product, productInventory);
	}
}
