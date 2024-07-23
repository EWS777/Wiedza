namespace Wiedza.Api.Repositories;

public interface IUserSaltRepository
{
    /// <summary>
    /// Get a salt of the requested user
    /// </summary>
    /// <param name="userId">ID of the requested user</param>
    /// <returns>
    /// Person's salt if exists;
    /// otherwise <see langword="null"/>
    /// </returns>
    Task<string?> GetSaltAsync(Guid userId);
    /// <summary>
    /// Creates salt for the user
    /// </summary>
    /// <param name="userId">ID of the user</param>
    /// <returns>Created salt for the user</returns>
    Task<string> AddPersonSalt(Guid userId);
}