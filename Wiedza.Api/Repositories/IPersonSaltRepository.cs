namespace Wiedza.Api.Repositories;

public interface IPersonSaltRepository
{
    /// <summary>
    /// Get a salt of the requested person
    /// </summary>
    /// <param name="personId">ID of the requested person</param>
    /// <returns>
    /// Person's salt if exists;
    /// otherwise <see langword="null"/>
    /// </returns>
    public Task<string?> GetSaltAsync(Guid personId);
    /// <summary>
    /// Creates salt for the person
    /// </summary>
    /// <param name="personId">ID of the person</param>
    /// <returns>Created salt for the person</returns>
    public Task<string> AddPersonSalt(Guid personId);
}