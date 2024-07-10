using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Wiedza.Core.Exceptions;

namespace Wiedza.Api.Services;

public sealed class ExceptionHandlerService(ProblemDetailsFactory problemDetailsFactory)
{
    public ActionResult HandleException(Exception exception, HttpContext? context = null)
    {
        var statusCode = exception switch
        {
            NotFoundException => (int)HttpStatusCode.NotFound,
            BadRequestException => (int)HttpStatusCode.BadRequest,
            UnauthorizeException => (int)HttpStatusCode.Unauthorized,
            _ => (int)HttpStatusCode.InternalServerError
        };

        string? instance = null;

        if (context is { Request: { } request }) instance = $"{request.Method} {request.Path}";

        var problemDetails = new ProblemDetails()
        {
            Status = statusCode,
            Title = exception.GetType().Name,
            Detail = exception.Message,
            Instance = instance
        };

        if (context is null)
        {
            return new ObjectResult(problemDetails)
            {
                StatusCode = statusCode
            };
        }
        var details = problemDetailsFactory.CreateProblemDetails(context, statusCode, problemDetails.Title,
            detail: problemDetails.Detail, instance: problemDetails.Instance);

        return new ObjectResult(details)
        {
            StatusCode = statusCode
        };
    }

    public Task<IActionResult> HandleExceptionAsync(Exception exception, HttpContext? context = null)
    {
        return Task.FromResult<IActionResult>(HandleException(exception, context));
    }

    public ActionResult<T> HandleException<T>(Exception exception, HttpContext? context = null)
    {
        return new ActionResult<T>(HandleException(exception, context));
    }

    public Task<ActionResult<T>> HandleExceptionAsync<T>(Exception exception, HttpContext? context = null)
    {
        return Task.FromResult(HandleException<T>(exception, context));
    }
}