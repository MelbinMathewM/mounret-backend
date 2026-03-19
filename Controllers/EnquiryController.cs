using Microsoft.AspNetCore.Mvc;
using Mounret.API.DTOs;
using Mounret.API.Interfaces;

namespace Mounret.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnquiriesController : ControllerBase
    {
        private readonly IEnquiryService _service;

        public EnquiriesController(IEnquiryService service)
        {
            _service = service;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEnquiryDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

    }
}