using Catalog.Api.app.presentation.middlewares;
using Catalog.Api.app.plugins;
using Catalog.Api.app.infrastructure.database;
using Catalog.Api.app.domain.datasources;
using Catalog.Api.app.infrastructure.datasources;
using Catalog.Api.app.domain.repositories;
using Catalog.Api.app.infrastructure.repositories;
using Catalog.Api.app.application.usecases.category;
using Catalog.Api.app.application.usecases.product;
using Catalog.Api.app.application.usecases.dashboard;
using Catalog.Api.app.application.usecases.images;
using Catalog.Api.app.configuration;


namespace Catalog.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // configuring link database
            builder.Services.AddScoped<IDBConnectionFactory, SqlConnectionFactory>();
            builder.Services.AddSingleton<CatalogConfiguration>();

            // repositories
            builder.Services.AddScoped<ICategoryRepository, CategoryRepositoryImp>();
            builder.Services.AddScoped<IProductRepository, ProductRepositoryImp>();
            builder.Services.AddScoped<IDashboarRepository, DashboardRepositoryImp>();


            // datasources
            builder.Services.AddScoped<ICategoryDatasource, CategoryDatasourceImp>();
            builder.Services.AddScoped<IProductDatasource, ProductDatasourceImp>();
            builder.Services.AddScoped<IDashboardDatasource, DashboardDatasourceImp>();
            builder.Services.AddScoped<IImageDatasource, CloudinaryPlugin>();


            // usecases of category
            builder.Services.AddScoped<FindCategoriesActiveUsecase>();
            builder.Services.AddScoped<FindCategoriesUsecase>();
            builder.Services.AddScoped<CreateCategoryUsecase>();
            builder.Services.AddScoped<FindCategoryByIdUsecase>();
            builder.Services.AddScoped<FindCategoryBySlugUsecase>();
            builder.Services.AddScoped<UpdateCategoryUsecase>();
            builder.Services.AddScoped<DeleteCategoryUsecase>();

            // usecases of products
            builder.Services.AddScoped<FindProductsUsecase>();
            builder.Services.AddScoped<FindProductsByActiveUsecase>();
            builder.Services.AddScoped<FindProductByIdUsecase>();
            builder.Services.AddScoped<FindProductBySlugActiveUsecase>();
            builder.Services.AddScoped<CreateProductUsecase>();
            builder.Services.AddScoped<DeleteProductUsecase>();
            builder.Services.AddScoped<UpdateProductUsecase>();

            // usecases of images
            builder.Services.AddScoped<UploadImageUsecase>();
            builder.Services.AddScoped<DeleteImageUsecase>();


            // usecases of dashboard
            builder.Services.AddScoped<SummaryDashboardUsecase>();


            // Add services to the container.
            builder.Services.AddControllers();


            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSingleton<JwtPlugin>();
            builder.Services.AddSingleton<CloudinaryPlugin>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            // app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<JwtMiddleware>();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
