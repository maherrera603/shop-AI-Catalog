using Catalog.Api.app.domain.datasources;
using Catalog.Api.app.domain.dtos.responses.dashboard;
using Catalog.Api.app.infrastructure.database;
using Dapper;
using System.Data;

namespace Catalog.Api.app.infrastructure.datasources;

public class DashboardDatasourceImp : IDashboardDatasource
{
    private readonly IDBConnectionFactory _connectionFactory;

    public DashboardDatasourceImp(IDBConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<SummaryResponse> Summary()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleAsync<SummaryResponse>(
            "sp_catalog_product_summary",
            commandType: CommandType.StoredProcedure
        );
    }
}
