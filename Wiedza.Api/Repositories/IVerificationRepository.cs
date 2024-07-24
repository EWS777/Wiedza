using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories;

public interface IVerificationRepository
{
    Task<Verification[]> GetAllVerifications();
    Task<Result<Verification>> GetVerificationAsync(Guid verificationId);
    Task<Verification> AddVerificationAsync(Verification verification);
    Task<Result<Verification>> UpdateVerificationStatusAsync(Guid verificationId, Action<Verification> update);
    Task<Result<bool>> DeleteVerificationAsync(Guid verificationId);
}