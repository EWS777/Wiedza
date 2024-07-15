using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Wiedza.Api.Services;
using Wiedza.Core.Exceptions;

namespace Wiedza.Api.Core.Extensions;

public static class ValidatableObjectExtensions
{
    private static string? GetValidationErrorMessage(this IValidatableObject validatable)
    {
        var errors = validatable.Validate(new ValidationContext(validatable)).ToArray();
        if (errors.Any() is false) return null;

        var errorMessageSb = new StringBuilder(errors.Length);

        foreach (var error in errors)
        {
            errorMessageSb.AppendFormat("Message: {0}", error.ErrorMessage ?? "Null");
            var strings = error.MemberNames.ToArray();
            if (strings.Length == 0) errorMessageSb.Append("; ");
            errorMessageSb.Append(" Member names: ").AppendJoin(", ", strings).Append("; ");
        }
        return errorMessageSb.Remove(errorMessageSb.Length-2, 2).ToString();
    }

    public static bool IsValidationFailed(this IValidatableObject validatable, out ValidationException exception)
    {
        var validationErrorMessage = validatable.GetValidationErrorMessage();
        if (validationErrorMessage is null)
        {
            exception = null!;
            return false;
        }

        exception = new ValidationException(validationErrorMessage);
        return true;
    }
}