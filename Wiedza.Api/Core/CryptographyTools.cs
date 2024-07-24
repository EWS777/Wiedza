using System.Security.Cryptography;
using System.Text;

namespace Wiedza.Api.Core;

public static class CryptographyTools
{
    public static string GenerateToken(int size = 64)
    {
        if (size <= 0) throw new ArgumentException("Size must be greeter than zero!", nameof(size));
        Span<byte> bytes = stackalloc byte[size];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes);
    }

    public static string GetPasswordHash(string passwordHash, string salt)
    {
        using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(passwordHash.ToLower(), Encoding.UTF8.GetBytes(salt),
            10_000, HashAlgorithmName.SHA256);

        var hashBytes = rfc2898DeriveBytes.GetBytes(256);
        var result = string.Concat(hashBytes.Select(p => p.ToString("X2")));
        return result;
    }
}