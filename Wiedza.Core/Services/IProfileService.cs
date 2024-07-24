using Wiedza.Core.Models;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Utilities;

namespace Wiedza.Core.Services;

public interface IProfileService
{
    Task<Result<Profile>> GetProfileAsync(Guid personId);
    Task<Result<Profile>> GetProfileAsync(string username);
    Task<Result<Profile>> UpdateProfileAsync(Guid personId, Action<Profile> update);
    Task<Result<bool>> DeleteProfileAsync(Guid personId, string passwordHash);
    Task<Result<Review>> AddReviewAsync(string username, Guid reviewAuthorId, AddReviewRequest addReviewRequest);
    Task<Result<Review[]>> GetReviewsAsync(string username);
}