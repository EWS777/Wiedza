using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data;
using Wiedza.Api.Data.Models;

namespace Wiedza.Api.Repositories.Implementations;

public class DbUserSaltRepository(DataContext dataContext) : IUserSaltRepository
{
    public async Task<string?> GetSaltAsync(Guid userId)
    {
        var personSalt = await dataContext.UserSalts.SingleOrDefaultAsync(p => p.UserId == userId);
        return personSalt?.Salt;
    }

    public async Task<string> AddPersonSalt(Guid userId)
    {
        var personSalt = new UserSalt()
        {
            UserId = userId,
            Salt = GenerateSalt()
        };
        await dataContext.UserSalts.AddAsync(personSalt);
        await dataContext.SaveChangesAsync();
        return personSalt.Salt;
    }

    #region Private

    private static string GenerateSalt()
    {
        Span<byte> bytes = stackalloc byte[16];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes);
    }

    #endregion
}