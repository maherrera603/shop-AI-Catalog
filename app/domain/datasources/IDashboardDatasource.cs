using Catalog.Api.app.domain.dtos.responses.dashboard;

namespace Catalog.Api.app.domain.datasources;

public interface IDashboardDatasource
{
    Task<SummaryResponse> Summary();
}
