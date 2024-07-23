using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;
using Administrator = Wiedza.Core.Models.Data.Administrator;

namespace Wiedza.Api.Repositories;

public interface IPersonRepository
{
    /// <summary>
    /// Get user by user id
    /// </summary>
    /// <param name="personId">ID of requested user</param>
    /// <returns><see cref="Person"/> if exists</returns>
    /// <exception cref="PersonNotFoundException"></exception>
    Task<Result<Person>> GetPersonAsync(Guid personId);
    /// <summary>
    /// Get user by username or email
    /// </summary>
    /// <param name="usernameOrEmail">Username or email of requested user</param>
    /// <returns><see cref="Person"/> if exists</returns>
    /// <exception cref="PersonNotFoundException"></exception>
    Task<Result<Person>> GetPersonAsync(string usernameOrEmail);
    /// <summary>
    /// Adds user to the repository
    /// </summary>
    /// <param name="user"><see cref="Person"/> to add</param>
    /// <returns>Added user if successful</returns>
    /// <exception cref="CreationException"></exception>
    Task<Result<Person>> AddPersonAsync(Person user);
    /// <summary>
    /// Updates the requested user
    /// </summary>
    /// <param name="personId">ID of the requested user</param>
    /// <param name="update">Action perfomed on a user</param>
    /// <returns>Updated <see cref="Person"/>> if successful</returns>
    /// <exception cref="PersonNotFoundException"></exception>
    Task<Result<Person>> UpdatePersonAsync(Guid personId, Action<Person> update);
    Task<Result<Verification>> VerifyProfileAsync(Verification verification);
    Task<bool> DeletePersonAsync(Guid personId);
    Task<Review> AddReviewAsync(Review review);
    Task<Review[]> GetReviewsAsync(Guid personId);
    Task<Result<Administrator>> GetAdministratorAsync(Guid adminId);
}