using Application.Interfaces;

namespace Infrastructure.Security;

public class BcryptPasswordHasher : IPasswordHasher
{
    public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);   // ⬅️ nuevo
    public bool Verify(string hash, string password) => BCrypt.Net.BCrypt.Verify(password, hash);
}
