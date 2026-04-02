using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pin.Products.Core.Services.Models
{
    public class CreateOrUpdateProductModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        [Required(ErrorMessage = "Please provide a Title!")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters!")]
        public required string Title { get; set; }

        [JsonPropertyName("price")]
        [Range(1, int.MaxValue, ErrorMessage = "Please provide a valid Price!")]
        public decimal Price { get; set; }

        [JsonPropertyName("description")]
        [Required(ErrorMessage = "Please provide a Description!")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters!")]
        public required string Description { get; set; }

        [JsonPropertyName("categoryId")]
        [Range(1, int.MaxValue, ErrorMessage = "Please provide a valid Category!")]
        public int CategoryId { get; set; }

        [JsonPropertyName("images")]
        public required List<string> Images { get; set; }
    }
}
