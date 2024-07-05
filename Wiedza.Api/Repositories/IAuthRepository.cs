using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories;

public interface IAuthRepository
{
    public Task<Person?> IsPersonCredentialsLegitAsync(string usernameOrEmail, string passwordHash);

    public Task<Result<Person>> RegisterPersonAsync(string username, string email, string passwordHash);
}