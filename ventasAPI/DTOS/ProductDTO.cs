using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ventasAPI.Services;

namespace ventasAPI.DTOS
{
    public class ProductDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public string ImageURL { get; set; }

    }
}
