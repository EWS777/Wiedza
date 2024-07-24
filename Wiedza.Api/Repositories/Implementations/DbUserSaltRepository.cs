using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Core;
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

    public async Task<string> AddPersonSalt(Guid userId, string? salt = null)
    {
        var personSalt = new UserSalt
        {
            UserId = userId,
            Salt = salt ?? CryptographyTools.GenerateToken(24)
        };
        await dataContext.UserSalts.AddAsync(personSalt);
        await dataContext.SaveChangesAsync();
        return personSalt.Salt;
    }
}