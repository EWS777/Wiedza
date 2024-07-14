using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories.Implementations;

public class DbPersonRepository(DataContext dataContext) : IPersonRepository
{
    public async Task<Result<Person>> GetPersonAsync(Guid personId)
    {
        var person = await dataContext.Persons.SingleOrDefaultAsync(p => p.Id == personId);
        if (person is null) return new PersonNotFoundException(personId);
        return person;
    }

    public async Task<Result<Person>> GetPersonAsync(string username)
    {
        var person = await dataContext.Persons.SingleOrDefaultAsync(p => p.Username == username);
        if (person is null) return new PersonNotFoundException(username);
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
}