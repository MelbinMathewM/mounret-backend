using Mounret.API.DTOs;
using Mounret.API.Interfaces;
using Mounret.API.Models;

namespace Mounret.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IWebHostEnvironment _env;

        public CategoryService(ICategoryRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();

            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Image = c.Image
            });
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
                return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Image = category.Image
            };
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            string imagePath = "";

            if (dto.Image != null)
            {
                var uploads = Path.Combine(_env.WebRootPath, "uploads/categories");

                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);

                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                imagePath = $"/uploads/categories/{fileName}";
            }

            var category = new Category
            {
                Name = dto.Name,
                Image = imagePath
            };

            var created = await _repository.AddAsync(category);

            return new CategoryDto
            {
                Id = created.Id,
                Name = created.Name,
                Image = created.Image
            };
        }

        public async Task<CategoryDto?> UpdateAsync(int id, CreateCategoryDto dto)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
                return null;

            category.Name = dto.Name;

            if (dto.Image != null)
            {
                var uploads = Path.Combine(_env.WebRootPath, "uploads/categories");

                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);

                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                category.Image = $"/uploads/categories/{fileName}";
            }

            var updated = await _repository.UpdateAsync(category);

            return new CategoryDto
            {
                Id = updated.Id,
                Name = updated.Name,
                Image = updated.Image
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
                return false;

            await _repository.DeleteAsync(category);

            return true;
        }
    }
}