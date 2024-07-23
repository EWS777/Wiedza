using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;
using Administrator = Wiedza.Core.Models.Data.Administrator;

namespace Wiedza.Api.Repositories;

public interface IPersonRepository
{
    /// <summary>
    /// Get person by person id
    /// </summary>
    /// <param name="personId">ID of requested person</param>
    /// <returns><see cref="Person"/> if exists</returns>
    /// <exception cref="PersonNotFoundException"></exception>
    Task<Result<Person>> GetPersonAsync(Guid personId);
    /// <summary>
    /// Get person by username or email
    /// </summary>
    /// <param name="usernameOrEmail">Username or email of requested person</param>
    /// <returns><see cref="Person"/> if exists</returns>
    /// <exception cref="PersonNotFoundException"></exception>
    Task<Result<Person>> GetPersonAsync(string usernameOrEmail);
    /// <summary>
    /// Adds person to the repository
    /// </summary>
    /// <param name="person"><see cref="Person"/> to add</param>
    /// <returns>Added person if successful</returns>
    /// <exception cref="CreationException"></exception>
    Task<Result<Person>> AddPersonAsync(Person person);
    /// <summary>
    /// Updates the requested person
    /// </summary>
    /// <param name="personId">ID of the requested person</param>
    /// <param name="update">Action perfomed on a person</param>
    /// <returns>Updated <see cref="Person"/>> if successful</returns>
    /// <exception cref="PersonNotFoundException"></exception>
    Task<Result<Person>> UpdatePersonAsync(Guid personId, Action<Person> update);
    Task<Result<Verification>> VerifyProfileAsync(Verification verification);
    Task<bool> DeletePersonAsync(Guid personId);
    Task<Review> AddReviewAsync(Review review);
    Task<Review[]> GetReviewsAsync(Guid personId);
    Task<Result<Administrator>> GetAdministratorAsync(Guid adminId);
}