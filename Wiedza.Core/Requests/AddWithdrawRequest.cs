using System.ComponentModel.DataAnnotations;

namespace Wiedza.Core.Requests;

public class AddWithdrawRequest : IValidatableObject
{
    public float Amount { get; set; }
    [CreditCard]public string CardNumber { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();

        if (Amount <= 0) errors.Add(new ValidationResult($"{nameof(Amount)} must be greater than zero!", [nameof(Amount)]));

        return errors;
    }
}