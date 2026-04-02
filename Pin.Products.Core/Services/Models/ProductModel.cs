using System.Text.Json.Serialization;

namespace Pin.Products.Core.Services.Models
{
    public class ProductModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public required string Title { get; set; }

        [JsonPropertyName("slug")]
        public required string Slug { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("description")]
        public required string Description { get; set; }

        [JsonPropertyName("category")]
        public required CategoryModel Category { get; set; }

        [JsonPropertyName("images")]
        public required List<string> Images { get; set; }
    }
}
