using Wiedza.Core.Models.Data.Base;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories;

public interface IUserRepository
{
    Task<Result<User>> GetUserAsync(string usernameOrEmail);
}