using Wiedza.Core.Models.Data;

namespace Wiedza.Core.Models;

public class Profile
{
    public Guid PersonId { get; set; }
    public string Username { get; set; }
    public string? Name { get; set; }
    public string Email { get; set; }
    public string? Description { get; set; }
    public byte[]? AvatarBytes { get; set; }

    public Profile(Person person)
    {
        PersonId = person.Id;
        Username = person.Username;
        Name = person.Name;
        Email = person.Email;
        Description = person.Description;
        AvatarBytes = person.AvatarBytes;
    }
}