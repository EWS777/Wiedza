using System.ComponentModel.DataAnnotations;

namespace Wiedza.Core.Requests;

public class RegisterRequest : IValidatableObject
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();

        if (string.IsNullOrWhiteSpace(Username))
            errors.Add(new ValidationResult($"{nameof(Username)} is empty!", [nameof(Username)]));

        if (string.IsNullOrWhiteSpace(Email))
            errors.Add(new ValidationResult($"{nameof(Email)} is empty!", [nameof(Email)]));

        if (string.IsNullOrWhiteSpace(PasswordHash))
            errors.Add(new ValidationResult($"{nameof(PasswordHash)} is empty!", [nameof(PasswordHash)]));

        return errors;
    }
}