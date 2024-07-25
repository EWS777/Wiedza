using Wiedza.Api.Repositories;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Data.Base;
using Wiedza.Core.Requests;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Services;

public class DbReviewService(
    IReviewRepository reviewRepository,
    IPersonRepository personRepository,
    IUserRepository userRepository) : IReviewService
{
    public async Task<Result<Review[]>> GetReviewsAsync(string username)
    {
        var personResult = await personRepository.GetPersonAsync(username);
        if (personResult.IsFailed) return personResult.Exception;

        return await reviewRepository.GetReviewsAsync(personResult.Value.Id);
    }

    public async Task<Review[]> GetAllReviewsAsync()
    {
        return await reviewRepository.GetAllReviewsAsync();
    }

    public async Task<Result<Review>> GetReviewAsync(Guid reviewId)
    {
        return await reviewRepository.GetReviewAsync(reviewId);
    }

    public async Task<Result<Review>> AddReviewAsync(string username, Guid reviewAuthorId,
        AddReviewRequest addReviewRequest)
    {
        var personResult = await personRepository.GetPersonAsync(username);
        if (personResult.IsFailed) return personResult.Exception;

        var person = personResult.Value;

        if (person.Id == reviewAuthorId) return new BadRequestException("You can't add review yourself");

        return await reviewRepository.AddReviewAsync(new Review
        {
            Message = addReviewRequest.Message,
            Rating = addReviewRequest.Rating,
            AuthorId = reviewAuthorId,
            PersonId = person.Id
        });
    }

    public async Task<Result<Review>> UpdateReviewAsync(string username, Guid reviewId, string message, float rating, Guid authorId)
    {
        var reviewResult = await reviewRepository.GetReviewAsync(reviewId);
        if (reviewResult.IsFailed) return reviewResult.Exception;

        var personResult = await personRepository.GetPersonAsync(username);
        if (personResult.IsFailed) return personResult.Exception;
        
        if (reviewResult.Value.Id != authorId) return new ForbiddenException("You are not an owner of this review!");

        if (rating is < 0.0f or > 5.0f) return new BadRequestException("Rating must be from 0 to 5!");

        if (message.Length > 250) return new BadRequestException("'Message' must be shorter than 250 letters!");

        return await reviewRepository.UpdateReviewAsync(reviewId, update =>
        {
            update.Message = message;
            update.Rating = rating;
        });
    }

    public async Task<Result<bool>> DeleteReviewAsync(string username, Guid reviewId, Guid authorId)
    {
        var reviewResult = await reviewRepository.GetReviewAsync(reviewId);
        if (reviewResult.IsFailed) return reviewResult.Exception;

        var userResult = await userRepository.GetUserAsync(authorId);
        if (userResult.IsFailed) return userResult.Exception;

        if (userResult.Value.UserType is UserType.Administrator || reviewResult.Value.Author.Username == username)
            return await reviewRepository.DeleteReviewAsync(reviewId);
        
        if (reviewResult.Value.Author.Username != username) return new ForbiddenException("You are not an owner of this review!");
        return new ForbiddenException("You don't have permissions to delete this review!");
    }
}