using Mounret.API.Models;

namespace Mounret.API.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int id);

        Task<Product> AddAsync(Product product);

        Task UpdateAsync(Product product);

        Task<(IEnumerable<Product> Products, int TotalCount)> GetPagedAsync(
            int page,
            int pageSize,
            int? categoryId,
            int? brandId,
            string? search,
            string? sortBy);
    }
}