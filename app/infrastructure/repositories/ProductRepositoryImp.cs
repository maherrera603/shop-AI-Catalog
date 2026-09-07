using Catalog.Api.app.domain.entities;
using Catalog.Api.app.domain.dtos.responses.product;
using Catalog.Api.app.domain.dtos.responses.pagination;
using Catalog.Api.app.domain.datasources;
using Catalog.Api.app.domain.repositories;

namespace Catalog.Api.app.infrastructure.repositories;

public class ProductRepositoryImp : IProductRepository{
    private readonly IProductDatasource _productDatasource;

    public ProductRepositoryImp(IProductDatasource productDatasource){
        _productDatasource = productDatasource;
    }

    public Task<ProductResponse> Create(Product product, ProductInventory productInventory)
    {
        return _productDatasource.Create(product, productInventory);
    }

    public Task<ProductResponse?> DeleteById(Guid id)
    {
        return _productDatasource.DeleteById(id);
    }

    public Task<PaginationResponse<ProductResponse>> Find()
    {
        return _productDatasource.Find();
    }

    public Task<List<ProductResponse>> FindActive()
    {
        return _productDatasource.FindActive();
    }

    public Task<List<ProductResponse>> FindByCategory(Guid categoryId)
    {
        return _productDatasource.FindByCategory(categoryId);
    }

    public Task<List<ProductResponse>> FindByCategoryActive(Guid categoryId)
    {
        return _productDatasource.FindByCategoryActive(categoryId);
    }

    public Task<ProductResponse?> FindById(Guid id)
    {
        return _productDatasource.FindById(id);
    }

    public Task<ProductResponse?> FindBySKu(string sku)
    {
        return _productDatasource.FindBySKu(sku);
    }
    public Task<ProductResponse?> FindBySlug(string slug)
    {
        return _productDatasource.FindBySlug(slug);
    }

    public Task<ProductResponse?> FindBySlugActive(string slug)
    {
        return _productDatasource.FindBySlugActive(slug);
    }

    public Task<ProductResponse> Update(Guid id, ProductResponse product, ProductInventory productInventory)
    {
        return _productDatasource.Update(id, product, productInventory);
    }
}


