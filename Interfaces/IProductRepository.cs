using Mounret.API.Models;

namespace Mounret.API.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> AddAsync(Product product);

        Task<(IEnumerable<Product> Products, int TotalCount)> GetPagedAsync(
            int page,
            int pageSize,
            int? categoryId,
            string? search,
            string? sortBy);

        Task UpdateAsync(Product product);

    }
}
