using Pin.Products.Core.Services.Models;

namespace Pin.Products.Core.Services.Interfaces
{
    public interface IFileUploadService
    {
        Task<ResultModel<string>> UploadAsync(Stream fileStream, string fileName);
    }
}
