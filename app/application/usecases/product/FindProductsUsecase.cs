using Catalog.Api.app.domain.repositories;
using Catalog.Api.app.domain.dtos.responses.pagination;
using Catalog.Api.app.domain.dtos.responses.product;

namespace Catalog.Api.app.application.usecases.product;


public class FindProductsUsecase {
	private readonly IProductRepository _productRepository;

	public FindProductsUsecase(IProductRepository productRepository){
		_productRepository = productRepository;
	}

	public async Task<PaginationResponse<ProductResponse>> Execute(){
		return await _productRepository.Find();
	}
}


