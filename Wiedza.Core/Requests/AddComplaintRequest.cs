using System.ComponentModel.DataAnnotations;

namespace Wiedza.Core.Requests;

public class AddComplaintRequest : IValidatableObject
{
    [StringLength(50)] public string Title { get; set; }
    [StringLength(500)] public string Description { get; set; }
    [MaxLength(10 * 1024 * 1024), MinLength(1)] public byte[]? FileBytes { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();
        if (string.IsNullOrWhiteSpace(Title))
            errors.Add(new ValidationResult($"{nameof(Title)} is empty!", [nameof(Title)]));

        if (string.IsNullOrWhiteSpace(Description))
            errors.Add(new ValidationResult($"{nameof(Description)} is empty!", [nameof(Description)]));
        return errors;
    }
}