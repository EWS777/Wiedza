using System.ComponentModel.DataAnnotations;

namespace Wiedza.Core.Requests;

public class ChangePasswordRequest : IValidatableObject
{
    public string OldPasswordHash { get; set; }
    public string NewPasswordHash { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();

        if (string.IsNullOrWhiteSpace(OldPasswordHash))
            errors.Add(new ValidationResult($"{nameof(OldPasswordHash)} is empty!", [nameof(OldPasswordHash)]));

        if (string.IsNullOrWhiteSpace(NewPasswordHash))
            errors.Add(new ValidationResult($"{nameof(NewPasswordHash)} is empty!", [nameof(NewPasswordHash)]));

        return errors;
    }
}