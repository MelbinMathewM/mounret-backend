using System.ComponentModel.DataAnnotations;

namespace Mounret.API.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; } = "Customer"; // Customer or Admin

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
