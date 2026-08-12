using Catalog.Api.app.domain.repositories;
using Catalog.Api.app.domain.dtos.requests.product;
using Catalog.Api.app.domain.dtos.responses.product;
using Catalog.Api.app.domain.error;
using Catalog.Api.app.domain.entities;


namespace Catalog.Api.app.application.usecases.product;


public class UpdateProductUsecase {
	private readonly IProductRepository _productRespository;
	private readonly ICategoryRepository _categoryRepository;

	public UpdateProductUsecase(IProductRepository productRepository, ICategoryRepository categoryRepository){
		_productRespository = productRepository;
		_categoryRepository = categoryRepository;
	}

	public async Task<ProductResponse> Execute(Guid id, UpdateProductDTO updateProductDTO){
		
		var product = await _productRespository.FindById(id);
		if(product == null) throw CustomError.NotFound("El producto no se encuentra registrado");

		var category = await _categoryRepository.FindById(updateProductDTO.CategoryId);
		if(category == null) throw CustomError.NotFound("Seleccione una categoria disponible");

		// validar si existe el slug
		if(product.Slug != updateProductDTO.Slug){
			var slugProduct = await _productRespository.FindBySlug(updateProductDTO.Slug);
			if(slugProduct != null && slugProduct.Id != product.Id) throw CustomError.Conflict($"El producto con slug {updateProductDTO.Slug} ya existe");
		}


		// validar si existe el sku
		if(product.Sku != updateProductDTO.Sku){
			var skuProduct = await _productRespository.FindBySKu(updateProductDTO.Sku);
			if(skuProduct != null && skuProduct.Id != product.Id) throw CustomError.Conflict($"El producto con sku {updateProductDTO.Sku} ya existe");
		}

		product.Name = updateProductDTO.Name;
		product.Slug = updateProductDTO.Slug;
		product.ShortDescription = updateProductDTO.ShortDescription;
		product.Description = updateProductDTO.Description;
		product.Price = updateProductDTO.Price;
		product.Sku = updateProductDTO.Sku;
		product.IsActive = updateProductDTO.IsActive;

		var productInventory = new ProductInventory {
			Stock = updateProductDTO.Stock
		};

		
		return await _productRespository.Update(id, product, productInventory);
	}
}
