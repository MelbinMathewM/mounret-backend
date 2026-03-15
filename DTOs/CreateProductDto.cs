using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Mounret.API.DTOs
{
    public class CreateProductDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public string? Description { get; set; }

        public IFormFile? Image { get; set; }

        public string? Dimensions { get; set; }

        public string? Material { get; set; }

        public string? AdditionalInfo { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int Stock { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int BrandId { get; set; }
    }
}