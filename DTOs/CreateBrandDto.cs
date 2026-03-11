using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Mounret.API.DTOs
{
    public class CreateBrandDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}