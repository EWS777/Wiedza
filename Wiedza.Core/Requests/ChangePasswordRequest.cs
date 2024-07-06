namespace Wiedza.Core.Requests;

public class ChangePasswordRequest
{
    public string oldPassword { get; set; }
    public string newPassword { get; set; }
}