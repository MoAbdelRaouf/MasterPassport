using MasterPassport.Domain.Entities;

namespace MasterPassport.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user, List<string> roles);
    string? ValidateToken(string token);
}
