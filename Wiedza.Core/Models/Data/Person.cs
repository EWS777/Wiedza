using Wiedza.Core.Models.Data.Base;
using Wiedza.Core.Models.Enums;

namespace Wiedza.Core.Models.Data;

public class Person : User
{
    public DateTimeOffset CreatedAt { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public float Balance { get; set; }
    public bool IsVerificated { get; set; }
    public float? Rating { get; set; }
    public byte[]? AvatarBytes { get; set; }
    public AccountState AccountState { get; set; }
}