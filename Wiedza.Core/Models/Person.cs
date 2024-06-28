using System.ComponentModel.DataAnnotations;
using Wiedza.Core.Models.Enums;

namespace Wiedza.Core.Models;

public class Person
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }
    [EmailAddress] public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string? Description { get; set; }
    public float Balance { get; set; }
    public bool IsVerificated { get; set; }
    [Range(0, 5)] public float Rating { get; set; }
    public byte[]? AvatarBytes { get; set; }
    public AccountState AccountState { get; set; }
}