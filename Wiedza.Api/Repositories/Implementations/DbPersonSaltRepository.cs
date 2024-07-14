using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data;
using Wiedza.Api.Data.Models;

namespace Wiedza.Api.Repositories.Implementations;

public class DbPersonSaltRepository(DataContext dataContext) : IPersonSaltRepository
{
    public async Task<string?> GetSaltAsync(Guid personId)
    {
        var personSalt = await dataContext.PersonSalts.SingleOrDefaultAsync(p => p.PersonId == personId);
        return personSalt?.Salt;
    }

    public async Task<string> AddPersonSalt(Guid personId)
    {
        var personSalt = new PersonSalt()
        {
            PersonId = personId,
            Salt = GenerateSalt()
        };
        await dataContext.PersonSalts.AddAsync(personSalt);
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