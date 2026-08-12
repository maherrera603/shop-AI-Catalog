using Catalog.Api.app.domain.repositories;
using Catalog.Api.app.domain.dtos.responses.product;
using Catalog.Api.app.domain.error;

namespace Catalog.Api.app.application.usecases.product;


public class FindProductByIdUsecase {
	private readonly IProductRepository _productRepository;

	public FindProductByIdUsecase(IProductRepository productRepository){
		_productRepository = productRepository;
	}

	public async Task<ProductResponse> Execute(Guid id){
		ProductResponse? product = await _productRepository.FindById(id);
		if(product == null) throw CustomError.NotFound("El producto no fue encontrado");
		return product;
	}
}


