using System.Text.Json.Serialization;

namespace Pin.Products.Core.Services.Models
{
    public class CategoryModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("slug")]
        public required string Slug { get; set; }

        [JsonPropertyName("image")]
        public required string Image { get; set; }
    }
}
