﻿namespace Wiedza.Core.Responses;

public class LoginResponse
{
    public string AuthorizationToken { get; set; }
    public Guid UserId { get; set; }
    public string Role { get; set; }
    public long ExpiresAt { get; set; }
}