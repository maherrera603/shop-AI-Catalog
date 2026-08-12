using Catalog.Api.app.domain.repositories;
using Catalog.Api.app.domain.dtos.responses.product;
using Catalog.Api.app.domain.error;

namespace Catalog.Api.app.application.usecases.product;

public class DeleteProductUsecase {
	private readonly IProductRepository _productRepository;

	public DeleteProductUsecase(IProductRepository productRepository){
		_productRepository = productRepository;
	}

	public async Task<ProductResponse> Execute(Guid id){

		var product = await _productRepository.FindById(id);
		if(product == null) throw CustomError.NotFound("el producto no se encuentra registrado");
		
		return await _productRepository.DeleteById(id);
	}
}
