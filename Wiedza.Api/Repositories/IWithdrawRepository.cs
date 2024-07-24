using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories;

public interface IWithdrawRepository
{
    Task<Withdraw[]> GetAllWithdrawsAsync();
    Task<Withdraw[]> GetPersonWithdrawsAsync(Guid personId);
    Task<Result<Withdraw>> GetWithdrawAsync(Guid withdrawId);
    Task<Withdraw> AddWithdrawAsync(Withdraw withdraw);
    Task<Result<Withdraw>> UpdateWithdrawAsync(Guid withdrawId, Action<Withdraw> update);
    Task<bool> DeleteWithdrawAsync(Guid withdrawId);
}