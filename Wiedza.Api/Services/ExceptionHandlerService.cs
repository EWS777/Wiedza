using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Wiedza.Core.Exceptions;

namespace Wiedza.Api.Services;

public sealed class ExceptionHandlerService(ProblemDetailsFactory problemDetailsFactory)
{
    public ActionResult HandleException(Exception exception, HttpContext context)
    {
        var statusCode = exception switch
        {
            NotFoundException => (int)HttpStatusCode.NotFound,
            BadRequestException => (int)HttpStatusCode.BadRequest,
            UnauthorizedException => (int)HttpStatusCode.Unauthorized,
            ValidationException => (int)HttpStatusCode.BadRequest,
            ForbiddenException => (int)HttpStatusCode.Forbidden,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var request = context.Request;

        var instance = $"{request.Method} {request.Path}";

        var details = problemDetailsFactory.CreateProblemDetails(context, statusCode, exception.GetType().Name,
            detail: exception.Message, instance: instance);
        
        details.Extensions.Add("extensions", new
        {
            Message = exception.Message,
            Source = exception.TargetSite?.ReflectedType?.FullName ?? exception.Source,
            StackTrace = exception.StackTrace
        });

        return new ObjectResult(details)
        {
            StatusCode = statusCode
        };
    }
}