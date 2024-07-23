using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories;

public interface IWithdrawRepository
{
    Task<Result<Withdraw>> GetWithdrawAsync(Guid withdrawId);
    Task<Result<Withdraw>> AddWithdrawAsync(Withdraw withdraw);
    Task<Result<Withdraw[]>> GetWithdrawsAsync(Guid userId);
    Task<Result<Withdraw[]>> GetAdminWithdrawsAsync();
    Task<bool> DeleteWithdrawAsync(Guid withdrawId);
    Task<Result<Withdraw>> UpdateWithdrawStatusAsync(Guid withdrawId, Action<Withdraw> update);
}