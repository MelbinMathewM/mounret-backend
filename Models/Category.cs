using System.ComponentModel.DataAnnotations;

namespace Mounret.API.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Product>? Products { get; set; }
        
    }
}