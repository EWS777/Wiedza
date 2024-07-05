using Microsoft.AspNetCore.Mvc;
using Wiedza.Core.Exceptions;

namespace Wiedza.Api.Services;

public sealed class ExceptionHandlerService
{
    public ActionResult HandleException(Exception exception)
    {
        return exception switch
        {
            NotFoundException => new NotFoundObjectResult(exception),
            BadRequestException => new BadRequestObjectResult(exception),
            _ => throw exception
        };
    }

    public Task<IActionResult> HandleExceptionAsync(Exception exception) => Task.FromResult<IActionResult>(HandleException(exception));

    public ActionResult<T> HandleException<T>(Exception exception) => new(HandleException(exception));

    public Task<ActionResult<T>> HandleExceptionAsync<T>(Exception exception) => Task.FromResult(HandleException<T>(exception));
}