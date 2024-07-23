using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Core.Services;

public interface IWithdrawService
{
    Task<Result<Withdraw>> GetWithdrawAsync(Guid userId, Guid withdrawId);
    Task<Result<Withdraw>> AddWithdrawAsync(Guid userId, float amount, ulong cardNumber);
    Task<Result<Withdraw[]>> GetWithdrawsAsync(Guid userId);
    Task<Result<bool>> DeleteWithdrawAsync(Guid userId, Guid withdrawId);
    Task<Result<Withdraw>> UpdateWithdrawStatusAsync(Guid userId, Guid withdrawId, bool isCompleted);
}