using Mounret.API.Models;

namespace Mounret.API.Interfaces
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetAllAsync();
        Task<Brand?> GetByIdAsync(int id);
        Task<Brand> AddAsync(Brand brand);
        Task DeleteAsync(Brand brand);
    }
}