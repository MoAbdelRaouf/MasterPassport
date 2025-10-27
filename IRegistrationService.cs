using MasterPassport.Application.DTOs;

namespace MasterPassport.Application.Interfaces;

public interface IRegistrationService
{
    Task<ApiResponse<int>> RegisterUserAsync(RegistrationRequestDto request);
    Task<ApiResponse<bool>> ApproveUserAsync(int tempUserId, string approvedBy);
    Task<ApiResponse<bool>> RejectUserAsync(int tempUserId, string reason);
    Task<ApiResponse<List<TempUserSummaryDto>>> GetPendingRegistrationsAsync();
}

public class TempUserSummaryDto
{
    public int TempUserID { get; set; }
    public string FullNameEn { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public bool IsApproved { get; set; }
}
