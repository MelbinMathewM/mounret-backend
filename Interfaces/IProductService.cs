using Mounret.API.DTOs;

namespace Mounret.API.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(CreateProductDto dto);

        Task<object> GetPagedAsync(
            int page,
            int pageSize,
            int? categoryId,
            string? search,
            string? sortBy);

        Task<ProductDto?> UpdateAsync(int id, CreateProductDto dto);
        Task<bool> SoftDeleteAsync(int id);

    }
}
