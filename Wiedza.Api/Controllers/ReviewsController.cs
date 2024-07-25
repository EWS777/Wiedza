using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wiedza.Api.Core;
using Wiedza.Api.Core.Extensions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Controllers;

[ApiController, Route("[controller]")]
public class ReviewsController(
    IReviewService reviewService) : ControllerBase
{
    [HttpGet, Route("{username}/reviews")]
    public async Task<ActionResult<Review[]>> GetReviews(string username)
    {
        var result = await reviewService.GetReviewsAsync(username);
        foreach (var review in result.Value)
        {
            review.Administrator = null;
            review.AdministratorId = null;
        }
        
        return result.Match(reviews => reviews, e => throw e);
    }

    [HttpGet, Route("all"), Authorize(Policy = Policies.AdminPolicy)]
    public async Task<ActionResult<Review[]>> GetAllReviews()
    {
        return await reviewService.GetAllReviewsAsync();
    }

    [HttpGet, Route("{reviewId:guid}")]
    public async Task<Result<Review>> GetReview(Guid reviewId)
    {
        var result = await reviewService.GetReviewAsync(reviewId);
        result.Value.Administrator = null;
        result.Value.AdministratorId = null;
        return result;
    }

    [HttpPost, Route("{username}/reviews/add"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<ActionResult<Review>> AddReview(string username, AddReviewRequest addReviewRequest)
    {
        var userId = User.Claims.GetUserId();
        var result = await reviewService.AddReviewAsync(username, userId, addReviewRequest);
        return result.Match(review => review, e => throw e);
    }

    [HttpPost, Route("{username}/reviews/{reviewId:guid}/change"), Authorize(Policy = Policies.PersonPolicy)]
    public async Task<ActionResult<Review>> UpdateReview(string username, Guid reviewId, [FromQuery] string message, [FromQuery] float rating)
    {
        var authorId = User.Claims.GetUserId();
        var result = await reviewService.UpdateReviewAsync(username, reviewId, message, rating, authorId);
        return result.Match( res => res, e => throw e);
    }

    [HttpDelete, Route("{username}/reviews/{reviewId:guid}"), Authorize(Policy = Policies.AdminAndPersonPolicy)]
    public async Task<IActionResult> DeleteReview(string username, Guid reviewId)
    {
        var authorId = User.Claims.GetUserId();
        var result = await reviewService.DeleteReviewAsync(username, reviewId, authorId);
        return result.Match(_ => Ok("Review was deleted!"), e => throw e);
    }
}