using System.ComponentModel.DataAnnotations;

namespace Wiedza.Core.Requests;

public class LoginRequest : IValidatableObject
{
    public string UsernameOrEmail { get; set; }
    public string PasswordHash { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();

        if (string.IsNullOrWhiteSpace(UsernameOrEmail))
            errors.Add(new ValidationResult($"{nameof(UsernameOrEmail)} was empty!", [nameof(UsernameOrEmail)]));

        if (string.IsNullOrWhiteSpace(PasswordHash))
            errors.Add(new ValidationResult($"{nameof(PasswordHash)} was empty!", [nameof(PasswordHash)]));

        return errors;
    }
}