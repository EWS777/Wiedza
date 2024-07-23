using System.Text.Json.Serialization;
using Wiedza.Core.Models.Enums;

namespace Wiedza.Core.Models.Data.Base;

public abstract class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public AccountState AccountState { get; set; }

    [JsonIgnore] public Type UserType { get; }
}