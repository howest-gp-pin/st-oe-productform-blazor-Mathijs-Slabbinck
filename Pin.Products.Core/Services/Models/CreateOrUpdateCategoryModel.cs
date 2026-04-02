using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pin.Products.Core.Services.Models
{
    public class CreateOrUpdateCategoryModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Please provide a Name!")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters!")]
        public required string Name { get; set; }

        [JsonPropertyName("image")]
        [Required(ErrorMessage = "Please provide an Image URL!")]
        public required string Image { get; set; }
    }
}
