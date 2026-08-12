using Catalog.Api.app.domain.datasources;
using Catalog.Api.app.domain.dtos.responses.dashboard;
using Catalog.Api.app.domain.repositories;

namespace Catalog.Api.app.infrastructure.repositories;

public class DashboardRepositoryImp : IDashboarRepository
{
    private readonly IDashboardDatasource _dashboardDatasource;
    public DashboardRepositoryImp(IDashboardDatasource dashboardDatasource) {
        _dashboardDatasource = dashboardDatasource;
    }

    public Task<SummaryResponse> Summary()
    {
        return _dashboardDatasource.Summary();
    }
}
