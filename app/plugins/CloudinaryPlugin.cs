using CloudinaryDotNet.Actions;
using Catalog.Api.app.domain.datasources;
using Catalog.Api.app.domain.error;
using Catalog.Api.app.configuration;
using Catalog.Api.app.domain.dtos.responses.images;

namespace  Catalog.Api.app.plugins;


public class CloudinaryPlugin : IImageDatasource{
    private CloudinaryDotNet.Cloudinary _cloudinary;


    public CloudinaryPlugin(CatalogConfiguration configuration){
        var account = new CloudinaryDotNet.Account(
            configuration.CloudinaryName,
            configuration.CloudinaryApiKey,
            configuration.CloudinaryApiSecret
        );

        _cloudinary = new CloudinaryDotNet.Cloudinary(account);
    }

    public async Task<bool> DeleteImageAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);

        var result = await _cloudinary.DestroyAsync(deleteParams);
        if(result.Error != null ) throw CustomError.Conflict(result.Error.Message);

        return result.Result == "ok";
    }

    public async Task<ImageResponse> UploadImageAsync(Stream fileStream, string filenameOriginal, string folder)
    {
        var uploadParams = new ImageUploadParams{
            File = new CloudinaryDotNet.FileDescription(filenameOriginal, fileStream),
            Folder = folder,
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        if(result.Error != null) throw CustomError.Conflict(result.Error.Message);

        return new ImageResponse{
            SecureUrl = result.SecureUrl.ToString(),
            PublicId = result.PublicId
        };
    }

}
