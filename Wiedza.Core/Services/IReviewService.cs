using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Utilities;

namespace Wiedza.Core.Services;

public interface IReviewService
{
    Task<Result<Review[]>> GetReviewsAsync(string username);
    Task<Review[]> GetAllReviewsAsync();
    Task<Result<Review>> GetReviewAsync(Guid reviewId);
    Task<Result<Review>> AddReviewAsync(string username, Guid reviewAuthorId, AddReviewRequest addReviewRequest);
    Task<Result<Review>> UpdateReviewAsync(string username, Guid reviewId, string message, float rating, Guid authorId);
    Task<Result<bool>> DeleteReviewAsync(string username, Guid reviewId, Guid authorId);
}