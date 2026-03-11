using Microsoft.AspNetCore.Mvc;
using Mounret.API.DTOs;
using Mounret.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace Mounret.API.Controllers
{


    [ApiController]
    [Route("api/catalogue")]
    public class CatalogueController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetCatalogue()
        {
            return Ok(new
            {
                brand = "MOUNRET",
                image = "/catalogue/book-cover.webp",
                downloadUrl = "/catalogue/mounret-catalogue.pdf"
            });
        }


        [HttpPost("request")]
        public IActionResult RequestCatalogue([FromBody] CatalogueRequestDto dto)
        {

            if (string.IsNullOrEmpty(dto.Email))
            {
                return BadRequest("Invalid request");
            }

            // Save request in DB if needed

            return Ok(new { message = "Request received" });
        }

    }
}