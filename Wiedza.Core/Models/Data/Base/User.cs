﻿namespace Wiedza.Core.Models.Data.Base;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
}