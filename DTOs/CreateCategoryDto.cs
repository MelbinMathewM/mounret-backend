using System.ComponentModel.DataAnnotations;

namespace Mounret.API.DTOs
{
    public class CreateCategoryDto
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
