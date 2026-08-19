using Catalog.Api.app.domain.dtos.responses.images;

namespace Catalog.Api.app.domain.datasources;

public interface IImageDatasource {
	Task<ImageResponse> UploadImageAsync(Stream fileStream, string filenameOriginal, string folder);
	Task<bool> DeleteImageAsync(string publicId);

}
