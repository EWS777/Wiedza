using System.ComponentModel.DataAnnotations;

namespace Wiedza.Core.Requests;

public class AddPublicationRequest : IValidatableObject
{
    public bool IsProject { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public Guid? CategoryId { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();

        if (Price <= 0.0) 
            errors.Add(new ValidationResult($"{nameof(Price)} should be greater than 0!", [nameof(Price)]));
        
        if (string.IsNullOrWhiteSpace(Title)) 
            errors.Add(new ValidationResult($"{nameof(Title)} is empty!", [nameof(Title)]));

        if (string.IsNullOrWhiteSpace(Description))
            errors.Add(new ValidationResult($"{nameof(Description)} is empty!", [nameof(Description)]));

        return errors;
    }
}