using Mounret.API.DTOs;
using Mounret.API.Interfaces;
using Mounret.API.Models;

namespace Mounret.API.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _repository;
        private readonly IWebHostEnvironment _env;

        public BrandService(IBrandRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public async Task<IEnumerable<BrandDto>> GetAllAsync()
        {
            var brands = await _repository.GetAllAsync();

            return brands.Select(b => new BrandDto
            {
                Id = b.Id,
                Name = b.Name,
                Image = b.Image
            });
        }

        public async Task<BrandDto?> GetByIdAsync(int id)
        {
            var brand = await _repository.GetByIdAsync(id);

            if (brand == null)
                return null;

            return new BrandDto
            {
                Id = brand.Id,
                Name = brand.Name,
                Image = brand.Image
            };
        }

        public async Task<BrandDto> CreateAsync(CreateBrandDto dto)
        {
            string imagePath = "";

            if (dto.Image != null)
            {
                var uploads = Path.Combine(_env.WebRootPath, "uploads/brands");

                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);

                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                imagePath = $"/uploads/brands/{fileName}";
            }

            var brand = new Brand
            {
                Name = dto.Name,
                Image = imagePath
            };

            var created = await _repository.AddAsync(brand);

            return new BrandDto
            {
                Id = created.Id,
                Name = created.Name,
                Image = created.Image
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var brand = await _repository.GetByIdAsync(id);

            if (brand == null)
                return false;

            await _repository.DeleteAsync(brand);

            return true;
        }
    }
}