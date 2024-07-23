using Wiedza.Core.Models;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Utilities;
using Administrator = Wiedza.Core.Models.Data.Administrator;

namespace Wiedza.Core.Services;

public interface IProfileService
{
    Task<Result<Profile>> GetProfileAsync(Guid personId);
    Task<Result<Profile>> GetProfileAsync(string username);
    Task<Result<Profile>> UpdateProfileAsync(Guid personId, Action<Profile> update);
    Task<Result<bool>> DeleteProfileAsync(Guid personId, string passwordHash);
    Task<Review> AddReviewAsync(Guid personId, Guid userId, AddReviewRequest addReviewRequest);
    Task<Review[]> GetReviewsAsync(Guid personId);
    Task<Result<Administrator>> GetAdministratorAsync(Guid adminId);
}