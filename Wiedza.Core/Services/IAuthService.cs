﻿using Wiedza.Core.Requests;
using Wiedza.Core.Responses;
using Wiedza.Core.Utilities;

namespace Wiedza.Core.Services;

public interface IAuthService
{
    public Task<Result<LoginResponse>> LoginAsync(LoginRequest request);
    public Task<Result<LoginResponse>> RegisterAsync(RegisterRequest request);
    public Task<Result<LoginResponse>> RefreshTokenAsync(string jwtToken);
    public Task<Result<bool>> ChangePasswordAsync(Guid personId, ChangePasswordRequest changePasswordRequest);
}