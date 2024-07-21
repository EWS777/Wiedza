using System.ComponentModel.DataAnnotations;

namespace Wiedza.Core.Requests;

public class RegisterRequest : IValidatableObject
{
    [StringLength(25)] public string Username { get; set; }
    [EmailAddress] public string Email { get; set; }
    public string PasswordHash { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();

        if (string.IsNullOrWhiteSpace(Username))
            errors.Add(new ValidationResult($"{nameof(Username)} is empty!", [nameof(Username)]));
        else
        {
            var chars = Username.AsSpan();
            foreach (var c in chars)
            {
                if (char.IsLetterOrDigit(c) || c is '_' or '.') continue;
                errors.Add(new ValidationResult(
                    $"{nameof(Username)} must contains only letters and digits or special symbols ('.', '_')!",
                    [nameof(Username)]));
                break;
            }
        }

        if (string.IsNullOrWhiteSpace(Email))
            errors.Add(new ValidationResult($"{nameof(Email)} is empty!", [nameof(Email)]));

        if (string.IsNullOrWhiteSpace(PasswordHash))
            errors.Add(new ValidationResult($"{nameof(PasswordHash)} is empty!", [nameof(PasswordHash)]));

        return errors;
    }
}