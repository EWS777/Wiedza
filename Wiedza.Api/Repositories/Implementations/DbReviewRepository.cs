using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories.Implementations;

public class DbReviewRepository(DataContext dataContext) : IReviewRepository
{
    public async Task<Review[]> GetReviewsAsync(Guid personId)
    {
        return await dataContext.Reviews
            .Include(x => x.Author)
            .AsNoTracking()
            .Where(x => x.PersonId == personId).ToArrayAsync();
    }

    public async Task<Review[]> GetAllReviewsAsync()
    {
        return await dataContext.Reviews.ToArrayAsync();
    }

    public async Task<Result<Review>> GetReviewAsync(Guid reviewId)
    {
        var review = await dataContext.Reviews.SingleOrDefaultAsync(x => x.Id == reviewId);
        if (review is null) return new ReviewNotFoundException(reviewId);

        return review;
    }

    public async Task<Review> AddReviewAsync(Review review)
    {
        await dataContext.Reviews.AddAsync(review);
        await dataContext.SaveChangesAsync();
        return review;
    }

    public async Task<Result<Review>> UpdateReviewAsync(Guid reviewId, Action<Review> update)
    {
        var result = await GetReviewAsync(reviewId);
        if (result.IsFailed) return result.Exception;

        update(result.Value);
        await dataContext.SaveChangesAsync();
        return result.Value;
    }

    public async Task<Result<bool>> DeleteReviewAsync(Guid reviewId)
    {
        var result = await dataContext.Reviews.SingleOrDefaultAsync(x => x.Id == reviewId);
        if (result is null) return false;
        dataContext.Reviews.Remove(result);
        await dataContext.SaveChangesAsync();

        return true;
    }
}