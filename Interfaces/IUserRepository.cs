using Mounret.API.Models;

namespace Mounret.API.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);

        Task<User?> GetByIdAsync(int id);

        Task<User> AddAsync(User user);
    }
}