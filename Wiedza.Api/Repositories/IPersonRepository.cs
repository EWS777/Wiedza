using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories;

public interface IPersonRepository
{
    public Task<Result<Person>> GetPersonAsync(Guid personId);
    public Task<Result<Person>> GetPersonAsync(string username);
    public Task<Result<Person>> UpdatePersonAsync(Guid personId, Action<Person> update);
}