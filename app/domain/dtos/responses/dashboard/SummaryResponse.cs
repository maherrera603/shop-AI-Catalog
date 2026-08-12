namespace Catalog.Api.app.domain.dtos.responses.dashboard;

public class SummaryResponse
{
    public int CategoriesTotal { get; set; }
    public int CategoriesCreatedThisMonth { get; set; }
    public int ProductsTotal { get; set; }
    public int ProductsCreatedThisMonth { get; set; }
}
