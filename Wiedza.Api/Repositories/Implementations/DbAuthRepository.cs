using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories.Implementations;

public class DbAuthRepository(DataContext dataContext) : IAuthRepository
{
    public async Task<Person?> IsPersonCredentialsLegitAsync(string usernameOrEmail, string passwordHash)
    {
        return await dataContext.Persons.FirstOrDefaultAsync(p =>
            (p.Username == usernameOrEmail || p.Email == usernameOrEmail) &&
            p.PasswordHash == passwordHash
        );
    }

    public async Task<Result<Person>> RegisterPersonAsync(string username, string email, string passwordHash)
    {
        var usernameAny = await dataContext.Persons.AnyAsync(p => p.Username == username);
        if (usernameAny) return new InvalidCredentialsException("Username is taken!");

        var emailAny = await dataContext.Persons.AnyAsync(p => p.Email == email);
        if (emailAny) return new InvalidCredentialsException("Email is taken!");

        var person = new Person
        {
            Username = username,
            Email = email,
            PasswordHash = passwordHash
        };
        await dataContext.Persons.AddAsync(person);
        await dataContext.SaveChangesAsync();
        return person;
    }
}