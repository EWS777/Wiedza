using System.Text.Json.Serialization;
using Wiedza.Core.Attributes;
using Wiedza.Core.Models.Enums;

namespace Wiedza.Core.Models.Data.Base;

public abstract class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    [JsonIgnore] public string PasswordHash { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public AccountState AccountState { get; set; }

    [JsonIgnore] public UserType UserType { get; }
}

public enum UserType
{
    [EnumTypeValue(typeof(Person))]
    Person,
    [EnumTypeValue(typeof(Administrator))]
    Administrator
}