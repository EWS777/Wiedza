using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories;

public interface IReviewRepository
{
    Task<Review[]> GetReviewsAsync(Guid personId);
    Task<Review[]> GetAllReviewsAsync();
    Task<Result<Review>> GetReviewAsync(Guid reviewId);
    Task<Review> AddReviewAsync(Review review);
    Task<Result<Review>> UpdateReviewAsync(Guid reviewId, Action<Review> update);
    Task<Result<bool>> DeleteReviewAsync(Guid reviewId);
}