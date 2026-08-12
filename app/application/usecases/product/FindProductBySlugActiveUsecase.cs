using Catalog.Api.app.domain.dtos.responses.product;
using Catalog.Api.app.domain.repositories;
using Catalog.Api.app.domain.error;

namespace Catalog.Api.app.application.usecases.product;

public class FindProductBySlugActiveUsecase {
	private readonly IProductRepository _productRepository;

	public FindProductBySlugActiveUsecase(IProductRepository productRepository){
		_productRepository = productRepository;
	}

	public async Task<ProductResponse> Execute(string slug){
		ProductResponse? product = await _productRepository.FindBySlugActive(slug);
		if(product == null) throw CustomError.NotFound("El producto no fue encontrado o no existe");

		return product;
	}
}
