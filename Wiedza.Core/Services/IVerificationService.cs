using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Utilities;

namespace Wiedza.Core.Services;

public interface IVerificationService
{
    Task<Verification[]> GetAllVerificationsAsync();
    Task<Result<Verification>> GetVerificationAsync(Guid verificationId, Guid userId);
    Task<Verification> AddVerificationAsync(Guid userId, AddVerificationRequest request);
    Task<Result<Verification>> UpdateVerificationStatusAsync(Guid verificationId, Guid userId, bool isCompleted);
    Task<Result<bool>> DeleteVerificationAsync(Guid verificationId, Guid userId);
}