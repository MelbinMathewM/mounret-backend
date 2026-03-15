using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Mounret.API.DTOs
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }

        public IFormFile? Image { get; set; }
    }
}