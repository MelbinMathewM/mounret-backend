using Mounret.API.DTOs;

namespace Mounret.API.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
        Task<UserProfileDto?> GetProfileAsync(int userId);

    }
}
