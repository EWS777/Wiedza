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

        using var reader = new StreamReader(request.BodyReader.AsStream());

        details.Extensions.Add("error_details", new
        {
                Message = exception.Message,
                Source = exception.TargetSite?.ReflectedType?.FullName ?? exception.Source,
                StackTrace = exception.StackTrace
        });

        details.Extensions.Add("request_details", new
        {
            Headers = request.Headers,
            Query = request.Query,
            Body = reader.ReadToEnd()
        });

        return new ObjectResult(details)
        {
            StatusCode = statusCode
        };
    }
}