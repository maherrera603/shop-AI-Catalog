using Microsoft.AspNetCore.Mvc;
using Catalog.Api.app.domain.common;
using Catalog.Api.app.domain.dtos.responses.dashboard;
using Catalog.Api.app.application.usecases.dashboard;





namespace Catalog.Api.app.presentation.controllers;

[ApiController]
[Route("api/v1/dashboard")]
public class DashboardController: ControllerBase {
	private readonly SummaryDashboardUsecase _summaryDashboardUsecase;


	public DashboardController(SummaryDashboardUsecase summaryDashboardUsecase) {
		_summaryDashboardUsecase = summaryDashboardUsecase;
	}


	[HttpGet("summary")]
	public async Task<ApiResponse<SummaryResponse>> Summary(){

		var response = await _summaryDashboardUsecase.Execute();

		return ApiResponse<SummaryResponse>.Success(response, "summary");
	}
}
