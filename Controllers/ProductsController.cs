using Microsoft.AspNetCore.Mvc;
using Mounret.API.DTOs;
using Mounret.API.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Mounret.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        // GET PAGED PRODUCTS
        [HttpGet]
        public async Task<IActionResult> GetPaged(
            int page = 1,
            int pageSize = 10,
            int? categoryId = null,
            int? brandId = null,
            string? search = null,
            string? sortBy = null)
        {

            Console.WriteLine("Hello");

            var result = await _service.GetPagedAsync(
                page, pageSize, categoryId, brandId, search, sortBy);

            return Ok(result);
        }

        // GET PRODUCT BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // CREATE PRODUCT
        // [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProductDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        // UPDATE PRODUCT
        // [Authorize(Roles = "Admin")]
        [HttpPost("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CreateProductDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // DELETE PRODUCT (Soft delete)
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.SoftDeleteAsync(id);

            if (!success)
                return NotFound();

            return Ok(new { message = "Product deleted successfully" });
        }
    }
}