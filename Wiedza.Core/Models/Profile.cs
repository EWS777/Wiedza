using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Data.Base;

namespace Wiedza.Core.Models;

public class Profile : IValidatableObject
{
    [JsonPropertyName("person_id")] public Guid PersonId { get; set; }
    [JsonPropertyName("username")] public string Username { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("email")] public string Email { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; }
    [JsonPropertyName("avatar")] public byte[]? AvatarBytes { get; set; }

    public Profile(Person user)
    {
        PersonId = user.Id;
        Username = user.Username;
        Name = user.Name;
        Email = user.Email;
        Description = user.Description;
        AvatarBytes = user.AvatarBytes;
    }

    public Profile(Profile other)
    {
        PersonId = other.PersonId;
        Username = other.Username;
        Name = other.Name;
        Email = other.Email;
        Description = other.Description;
        AvatarBytes = other.AvatarBytes;
    }

public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();

        if (string.IsNullOrWhiteSpace(Username))
            errors.Add(new ValidationResult($"{nameof(Username)} is empty!", [nameof(Username)]));

        if (Name is not null && string.IsNullOrWhiteSpace(Name))
            errors.Add(new ValidationResult($"{nameof(Name)} is empty!", [nameof(Name)]));

        if (Description is not null && string.IsNullOrWhiteSpace(Description))
            errors.Add(new ValidationResult($"{nameof(Description)} is empty!", [nameof(Description)]));

        if(AvatarBytes is {Length:0})
            errors.Add(new ValidationResult($"{nameof(AvatarBytes)} is empty array!", [nameof(AvatarBytes)]));

        return errors;
    }
}