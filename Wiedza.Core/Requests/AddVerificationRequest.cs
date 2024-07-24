using System.ComponentModel.DataAnnotations;

namespace Wiedza.Core.Requests;

public class AddVerificationRequest : IValidatableObject
{
    [Length(11,11)] public string Pesel { get; set; }
    [MaxLength(50)] public string Name { get; set; }
    [MaxLength(50)] public string Surname { get; set; }
    [MinLength(1), MaxLength(10 * 1024 * 1024)] public byte[] ImageDocumentBytes { get; set; }
    
    
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();
        
        if (string.IsNullOrWhiteSpace(Name))
        {
            errors.Add(new ValidationResult($"{nameof(Name)} is empty!", [nameof(Name)]));
        }
        if (string.IsNullOrWhiteSpace(Surname))
        {
            errors.Add(new ValidationResult($"{nameof(Surname)} is empty!", [nameof(Surname)]));
        }
        
        return errors;
    }
}