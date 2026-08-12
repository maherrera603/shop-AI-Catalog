using Catalog.Api.app.domain.dtos.responses.dashboard;
using Catalog.Api.app.domain.repositories;

namespace Catalog.Api.app.application.usecases.dashboard;

public class SummaryDashboardUsecase
{
    private readonly IDashboarRepository _dashboarRepository;

    public SummaryDashboardUsecase(IDashboarRepository dashboarRepository)
    {
        _dashboarRepository = dashboarRepository;
    }


    public async Task<SummaryResponse> Execute()
    {
        return await _dashboarRepository.Summary();
    }
}