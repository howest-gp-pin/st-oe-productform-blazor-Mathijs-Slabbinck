using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pin.Products.Core.Services.Models
{
    public class CreateOrUpdateCategoryModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Please provide a Name!")]
        public string Name { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; }
    }
}
