using System.ComponentModel.DataAnnotations;

namespace Mounret.API.DTOs
{
    public class CreateProductDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int Stock { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
