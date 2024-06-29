namespace Wiedza.Core.Requests;

public class LoginRequest
{
    public string UsernameOrEmail { get; set; }
    public string PasswordHash { get; set; }
}