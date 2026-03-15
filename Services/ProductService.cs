using Mounret.API.DTOs;
using Mounret.API.Interfaces;
using Mounret.API.Models;

namespace Mounret.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IWebHostEnvironment _env;

        public ProductService(
            IProductRepository repository,
            ICategoryRepository categoryRepository,
            IBrandRepository brandRepository,
            IWebHostEnvironment env)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _env = env;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();

            return products.Select(MapProduct);
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return null;

            return MapProduct(product);
        }

        public async Task<object> GetPagedAsync(
            int page,
            int pageSize,
            int? categoryId,
            int? brandId,
            string? search,
            string? sortBy)
        {
            var (products, totalCount) =
                await _repository.GetPagedAsync(
                    page, pageSize, categoryId, brandId, search, sortBy);

            var result = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Image = p.Image,
                Dimensions = p.Dimensions,
                Material = p.Material,
                AdditionalInfo = p.AdditionalInfo,
                Price = p.Price,
                Stock = p.Stock,
                IsActive = p.IsActive,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? "",
                BrandId = p.BrandId,
                BrandName = p.Brand?.Name ?? ""
            });

            return new
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Data = result
            };
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
            var brand = await _brandRepository.GetByIdAsync(dto.BrandId);

            if (category == null)
                throw new Exception("Category not found");

            if (brand == null)
                throw new Exception("Brand not found");

            string imagePath = "";

            if (dto.Image != null)
            {
                var uploads = Path.Combine(_env.WebRootPath, "uploads/products");

                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.Image.CopyToAsync(stream);

                imagePath = $"/uploads/products/{fileName}";
            }

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId,
                BrandId = dto.BrandId,
                Image = imagePath,
                Dimensions = dto.Dimensions,
                Material = dto.Material,
                AdditionalInfo = dto.AdditionalInfo,
                IsActive = dto.IsActive
            };

            var created = await _repository.AddAsync(product);

            return MapProduct(created);
        }

        public async Task<ProductDto?> UpdateAsync(int id, CreateProductDto dto)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return null;

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
            product.CategoryId = dto.CategoryId;
            product.BrandId = dto.BrandId;
            product.Dimensions = dto.Dimensions;
            product.Material = dto.Material;
            product.AdditionalInfo = dto.AdditionalInfo;
            product.IsActive = dto.IsActive;

            if (dto.Image != null)
            {
                var uploads = Path.Combine(_env.WebRootPath, "uploads/products");

                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.Image.CopyToAsync(stream);

                product.Image = $"/uploads/products/{fileName}";
            }

            await _repository.UpdateAsync(product);

            return MapProduct(product);
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return false;

            product.IsDeleted = true;

            await _repository.UpdateAsync(product);

            return true;
        }

        private ProductDto MapProduct(Product p)
        {
            return new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Image = p.Image,
                Dimensions = p.Dimensions,
                Material = p.Material,
                AdditionalInfo = p.AdditionalInfo,
                Price = p.Price,
                Stock = p.Stock,
                IsActive = p.IsActive,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? "",
                BrandId = p.BrandId,
                BrandName = p.Brand?.Name ?? ""
            };
        }
    }
}