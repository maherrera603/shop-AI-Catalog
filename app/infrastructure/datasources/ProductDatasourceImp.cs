using System.Data;
using Dapper;
using Catalog.Api.app.domain.datasources;
using Catalog.Api.app.domain.dtos.responses.pagination;
using Catalog.Api.app.domain.dtos.responses.product;
using Catalog.Api.app.domain.entities;
using Catalog.Api.app.infrastructure.database;
using Catalog.Api.app.domain.dtos.requests.querys;

namespace Catalog.Api.app.infrastructure.datasources;


public class ProductDatasourceImp : IProductDatasource {
    private readonly IDBConnectionFactory _factory;

    public ProductDatasourceImp(IDBConnectionFactory factory){
        _factory = factory;
    }
    

    public async Task<ProductResponse> Create(Product product, ProductInventory productInventory)
    {
        using var connection = _factory.CreateConnection();
        return await connection.QuerySingleAsync<ProductResponse>(
            "sp_create_product",
            new {
                categoryId = product.CategoryId,
                name = product.Name,
                slug = product.Slug,
                shortDescription = product.ShortDescription,
                description = product.Description,
                price = product.Price,
                sku = product.Sku,
                stock = productInventory.Stock,
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<ProductResponse?> DeleteById(Guid id)
    {
        using var connection = _factory.CreateConnection();

        return await connection.QuerySingleOrDefaultAsync<ProductResponse>(
            "sp_product_delete_by_id",
            new { id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<PaginationResponse<ProductResponse>> Find(QueryParams queryParams)
    {
        using var connection = _factory.CreateConnection();

        var parameters = new DynamicParameters();
        parameters.Add("@Page", queryParams.Page);
        parameters.Add("@PageSize", queryParams.PageSize);
        parameters.Add("@Status", queryParams.isActive);
        parameters.Add("@Search", queryParams.Search);

        var result = await connection.QueryMultipleAsync(
            "sp_products",
            parameters,
            commandType: CommandType.StoredProcedure
        );

        var totalItems = await result.ReadSingleAsync<int>();
        var products = (await result.ReadAsync<Product>()).ToList();

        return new PaginationResponse<ProductResponse>{
            TotalItems = totalItems,
            Items = products.Select(p => new ProductResponse(p)).ToList()
        };
    }

    public async Task<List<ProductResponse>> FindActive()
    {
        using var connection = _factory.CreateConnection();

        var products = await connection.QueryAsync<ProductResponse>(
            "sp_products_by_active",
            commandType: CommandType.StoredProcedure
        );

        return products.ToList();
    }

    public Task<List<ProductResponse>> FindByCategory(Guid categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProductResponse>> FindByCategoryActive(Guid categoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductResponse?> FindById(Guid id)
    {
        using var connection = _factory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<ProductResponse>(
            "sp_product_by_id",
            new { id}, 
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<ProductResponse?> FindBySKu(string sku)
    {
        using var connection = _factory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<ProductResponse>(
            "sp_product_by_sku",
            new { sku },
            commandType: CommandType.StoredProcedure
        );    
    }

    public async Task<ProductResponse?> FindBySlug(string slug)
    {
        using var connection = _factory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<ProductResponse>(
            "sp_product_by_slug",
            new { slug },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<ProductResponse?> FindBySlugActive(string slug)
    {
        using var connection = _factory.CreateConnection();

        return await connection.QuerySingleOrDefaultAsync<ProductResponse>(
            "sp_product_by_slug_active",
            new { slug },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<ProductResponse> Update(Guid id, ProductResponse product, ProductInventory productInventory)
    {
        using var connection = _factory.CreateConnection();

        return await connection.QuerySingleAsync<ProductResponse>(
            "sp_product_update_by_id",
            new {
                id = id,
                categoryId = product.CategoryId,
                name = product.Name,
                slug = product.Slug,
                shortDescription = product.ShortDescription,
                description = product.Description,
                price = product.Price,
                sku = product.Sku,
                isActive = product.IsActive,
                stock = productInventory.Stock                
            },
            commandType: CommandType.StoredProcedure
        );
    }
}
