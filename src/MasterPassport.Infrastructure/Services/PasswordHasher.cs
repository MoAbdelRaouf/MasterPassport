using System.Security.Cryptography;
using System.Text;
using MasterPassport.Application.Interfaces;

namespace MasterPassport.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    public (string hash, string salt) HashPassword(string password)
    {
        // Generate a random salt
        byte[] saltBytes = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }
        string salt = Convert.ToBase64String(saltBytes);

        // Hash the password with the salt
        string hash = HashPasswordWithSalt(password, salt);

        return (hash, salt);
    }

    public bool VerifyPassword(string password, string hash, string salt)
    {
        string hashedPassword = HashPasswordWithSalt(password, salt);
        return hashedPassword == hash;
    }

    private string HashPasswordWithSalt(string password, string salt)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password + salt);
            byte[] hashBytes = sha256.ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
