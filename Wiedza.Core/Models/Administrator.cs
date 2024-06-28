namespace Wiedza.Core.Models;

public class Administrator
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
}