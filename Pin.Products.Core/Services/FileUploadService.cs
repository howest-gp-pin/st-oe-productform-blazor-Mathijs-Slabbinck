using Microsoft.Extensions.Logging;
using Pin.Products.Core.Services.Interfaces;
using Pin.Products.Core.Services.Models;
using System.Net.Http.Json;

namespace Pin.Products.Core.Services
{
    public class FileUploadService : IFileUploadService
    {
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

        private readonly HttpClient _httpClient;
        private readonly ILogger<FileUploadService> _logger;

        public FileUploadService(HttpClient httpClient, ILogger<FileUploadService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ResultModel<string>> UploadAsync(Stream fileStream, string fileName)
        {
            ResultModel<string> resultModel = new ResultModel<string>();
            try
            {
                string safeFileName = Path.GetFileName(fileName);
                string extension = Path.GetExtension(safeFileName).ToLowerInvariant();

                if (!AllowedExtensions.Contains(extension))
                {
                    resultModel.Errors.Add("Only image files (JPG, PNG, GIF, WEBP) are allowed.");
                    return resultModel;
                }

                using MultipartFormDataContent content = new MultipartFormDataContent();
                StreamContent streamContent = new StreamContent(fileStream);
                content.Add(streamContent, "file", safeFileName);

                HttpResponseMessage result = await _httpClient.PostAsync(string.Empty, content);
                if (result.IsSuccessStatusCode)
                {
                    FileUploadResultModel? data = await result.Content.ReadFromJsonAsync<FileUploadResultModel>();
                    if (data is not null)
                    {
                        resultModel.Data = data.Location;
                        return resultModel;
                    }
                }

                resultModel.Errors.Add("File not uploaded!");
                return resultModel;
            }
            catch (HttpRequestException httpRequestException)
            {
                _logger.LogError(httpRequestException, "Error uploading file with extension {Extension}", Path.GetExtension(fileName));
                resultModel.Errors.Add("Connection error, please try again later.");
                return resultModel;
            }
        }
    }
}
