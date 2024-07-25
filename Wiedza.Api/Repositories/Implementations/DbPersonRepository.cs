using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories.Implementations;

public class DbPersonRepository(DataContext dataContext) : IPersonRepository
{
    public async Task<Person[]> GetPersonsAsync()
    {
        return await dataContext.Persons.ToArrayAsync();
    }

    public async Task<Result<Person>> GetPersonAsync(Guid personId)
    {
        var person = await dataContext.Persons.SingleOrDefaultAsync(p => p.Id == personId);
        if (person is null) return new PersonNotFoundException(personId);
        return person;
    }

    public async Task<Result<Person>> GetPersonAsync(string usernameOrEmail)
    {
        var person = await dataContext.Persons.SingleOrDefaultAsync(p => p.Username == usernameOrEmail
                                                                         || p.Email == usernameOrEmail);

        if (person is null) return new PersonNotFoundException(usernameOrEmail);
        return person;
    }

    public async Task<Result<Person>> AddPersonAsync(Person person)
    {
        if (await dataContext.Persons.AnyAsync(p => p.Email == person.Email))
            return new CreationException("Email is taken!");

        if (await dataContext.Persons.AnyAsync(p => p.Username == person.Username))
            return new CreationException("Username is taken!");

        await dataContext.Persons.AddAsync(person);
        await dataContext.SaveChangesAsync();
        return person;
    }

    public async Task<Result<Person>> UpdatePersonAsync(Guid personId, Action<Person> update)
    {
        var personResult = await GetPersonAsync(personId);
        if (personResult.IsFailed) return personResult.Exception;

        var person = personResult.Value;

        update(person);

        await dataContext.SaveChangesAsync();

        return person;
    }

    public async Task<Result<Person>> UpdatePersonStatusAsync(Guid personId, Action<Person> update)
    {
        var personResult = await GetPersonAsync(personId);
        if (personResult.IsFailed) return personResult.Exception;

        update(personResult.Value);
        await dataContext.SaveChangesAsync();
        return personResult.Value;
    }

    public async Task<bool> DeletePersonAsync(Guid personId)
    {
        var personResult = await GetPersonAsync(personId);
        if (personResult.IsFailed) return false;

        dataContext.Persons.Remove(personResult.Value);
        await dataContext.SaveChangesAsync();
        return true;
    }
}