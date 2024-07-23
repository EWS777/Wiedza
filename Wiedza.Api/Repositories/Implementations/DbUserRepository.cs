using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data.Base;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories.Implementations;

public class DbUserRepository(DataContext dataContext) : IUserRepository
{
    public async Task<Result<User>> GetUserAsync(string usernameOrEmail)
    {
        var user = await dataContext.Users.SingleOrDefaultAsync(p => p.Username == usernameOrEmail || p.Email == usernameOrEmail);
        if (user == null) return new UserNotFoundException(usernameOrEmail);

        return user;
    }
}