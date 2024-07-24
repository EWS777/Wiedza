using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Utilities;

namespace Wiedza.Core.Services;

public interface IWithdrawService
{
    Task<Withdraw[]> GetAllWithdrawsAsync();
    Task<Withdraw[]> GetPersonWithdrawsAsync(Guid personId);
    Task<Result<Withdraw>> GetWithdrawAsync(Guid withdrawId, Guid userId);

    Task<Result<Withdraw>> AddWithdrawAsync(Guid personId, AddWithdrawRequest request);
    Task<Result<Withdraw>> UpdateWithdrawStatusAsync(Guid withdrawId, Guid userId, bool isCompleted);
    Task<Result<bool>> DeleteWithdrawAsync(Guid withdrawId, Guid userId);
}