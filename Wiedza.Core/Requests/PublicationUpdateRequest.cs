using System.ComponentModel.DataAnnotations;
using Wiedza.Core.Models.Data.Base;
using Wiedza.Core.Models.Enums;

namespace Wiedza.Core.Requests;

public class PublicationUpdateRequest : IValidatableObject
{
    public string Title { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public PublicationUpdateStatus Status { get; set; }
    public Guid? CategoryId { get; set; }

    public PublicationUpdateRequest(PublicationBase publicationBase)
    {
        Title = publicationBase.Title;
        Description = publicationBase.Description;
        Price = publicationBase.Price;
        CategoryId = publicationBase.CategoryId;

        Status = publicationBase.Status switch
        {
            PublicationStatus.Active => PublicationUpdateStatus.Active,
            PublicationStatus.Inactive => PublicationUpdateStatus.Inactive,
            _ => PublicationUpdateStatus.Other
        };
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();

        if (string.IsNullOrWhiteSpace(Title))
            errors.Add(new ValidationResult($"{nameof(Title)} was empty!", [nameof(Title)]));
        if (string.IsNullOrWhiteSpace(Description))
            errors.Add(new ValidationResult($"{nameof(Description)} was empty!", [nameof(Description)]));
        if (Price <= 0)
            errors.Add(new ValidationResult($"{nameof(Price)} must be greater than 0!", [nameof(Price)]));

        return errors;
    }
}

public enum PublicationUpdateStatus
{
    Active, Inactive, Other
}