using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;
using Administrator = Wiedza.Core.Models.Data.Administrator;

namespace Wiedza.Api.Repositories.Implementations;

public class DbPersonRepository(DataContext dataContext) : IPersonRepository
{
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

    public async Task<Result<Person>> AddPersonAsync(Person user)
    {
        if(await dataContext.Persons.AnyAsync(p=>p.Email == user.Email))
            return new CreationException("Email is taken!");

        if (await dataContext.Persons.AnyAsync(p => p.Username == user.Username))
            return new CreationException("Username is taken!");

        await dataContext.Persons.AddAsync(user);
        await dataContext.SaveChangesAsync();
        return user;
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

    public async Task<Result<Verification>> VerifyProfileAsync(Verification verification)
    {
        await dataContext.Verifications.AddAsync(verification);
        await dataContext.SaveChangesAsync();
        return verification;
    }

    public async Task<bool> DeletePersonAsync(Guid personId)
    {
        var personResult = await GetPersonAsync(personId);
        if (personResult.IsFailed) return false;

        dataContext.Persons.Remove(personResult.Value);
        await dataContext.SaveChangesAsync();
        return true;
    }

    public async Task<Review> AddReviewAsync(Review review)
    {
        await dataContext.Reviews.AddAsync(review);
        await dataContext.SaveChangesAsync();
        return review;
    }

    public async Task<Review[]> GetReviewsAsync(Guid personId)
    {
        return await dataContext.Reviews
            .Include(x => x.Author)
            .AsNoTracking()
            .Where(x => x.PersonId == personId).ToArrayAsync();
    }

    public async Task<Result<Administrator>> GetAdministratorAsync(Guid adminId)
    {
        var person = await dataContext.Administrators.SingleOrDefaultAsync(x => x.Id == adminId);

        if (person is null) return new PersonNotFoundException(adminId);
        return person;
    }
}