using Mounret.API.DTOs;

namespace Mounret.API.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto dto);
        Task<string?> LoginAsync(LoginDto dto);
        Task<UserProfileDto?> GetProfileAsync(int userId);

    }
}
