namespace Wiedza.Core.Requests;

public class EditProfileRequest
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public byte[]? Avatar { get; set; }
}