using MasterPassport.Application.DTOs;

namespace MasterPassport.Application.Interfaces;

public interface IAuthenticationService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    Task<ApiResponse<bool>> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
    Task<ApiResponse<bool>> ResetPasswordAsync(string email);
}
