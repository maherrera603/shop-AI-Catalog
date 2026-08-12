using Catalog.Api.app.domain.repositories;
using Catalog.Api.app.domain.dtos.responses.product;

namespace Catalog.Api.app.application.usecases.product;


public class FindProductsByActiveUsecase {
	private readonly IProductRepository _productRepository;

	public FindProductsByActiveUsecase(IProductRepository productRepository){
		_productRepository = productRepository;
	}

	public async Task<List<ProductResponse>> Execute(){
		return await _productRepository.FindActive();
	}
}


