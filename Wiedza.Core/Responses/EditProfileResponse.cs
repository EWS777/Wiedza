namespace Wiedza.Core.Responses;

public class EditProfileResponse
{
    public string Username { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public byte[] Avatar { get; set; }
}