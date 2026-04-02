using System.Text.Json.Serialization;

namespace Pin.Products.Core.Services.Models
{
    public class FileUploadResultModel
    {
        [JsonPropertyName("originalname")]
        public required string OriginalName { get; set; }

        [JsonPropertyName("filename")]
        public required string FileName { get; set; }

        [JsonPropertyName("location")]
        public required string Location { get; set; }
    }
}
