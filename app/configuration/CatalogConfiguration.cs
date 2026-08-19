namespace Catalog.Api.app.configuration;

public class CatalogConfiguration {
    private readonly IConfiguration _configuration;

    public CatalogConfiguration(IConfiguration configuration){
        _configuration =  configuration;
    }

    public string CloudinaryName => _configuration["CloudinarySettings:CloudName"]
        ?? throw new InvalidOperationException("Cloudinary:CloudName no esta configurado");

    public string CloudinaryApiKey => _configuration["CloudinarySettings:ApiKey"]
        ?? throw new InvalidOperationException("Cloudinary:ApiKey no esta configurado");

    public string CloudinaryApiSecret => _configuration["CloudinarySettings:ApiSecret"]
        ?? throw new InvalidOperationException("Cloudinary:ApiSecret no esta configurado");
}
