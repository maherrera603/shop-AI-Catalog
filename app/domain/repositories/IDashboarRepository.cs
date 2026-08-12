using Catalog.Api.app.domain.dtos.responses.dashboard;

namespace Catalog.Api.app.domain.repositories;

public interface IDashboarRepository
{
    Task<SummaryResponse> Summary();
}
