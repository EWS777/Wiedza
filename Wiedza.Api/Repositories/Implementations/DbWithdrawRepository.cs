using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories.Implementations;

public class DbWithdrawRepository(DataContext dataContext) : IWithdrawRepository
{
    public async Task<Result<Withdraw>> GetWithdrawAsync(Guid withdrawId)
    {
        var withdraw = await dataContext.Withdraws
            .Include(x => x.Person)
            .SingleOrDefaultAsync(x => x.Id == withdrawId);
        if (withdraw is null) return new WithdrawNotFoundException(withdrawId);

        return withdraw;
    }

    public async Task<Result<Withdraw>> AddWithdrawAsync(Withdraw withdraw)
    {
        await dataContext.Withdraws.AddAsync(withdraw);
        await dataContext.SaveChangesAsync();
        return withdraw;
    }

    public async Task<Result<Withdraw[]>> GetWithdrawsAsync(Guid userId)
    {
        return await dataContext.Withdraws
            .Where(x => x.PersonId == userId)
            .AsNoTracking().ToArrayAsync();
    }

    public async Task<Result<Withdraw[]>> GetAdminWithdrawsAsync()
    {
        return await dataContext.Withdraws
            .Include(x=>x.Person)
            .AsNoTracking().ToArrayAsync();
    }

    public async Task<bool> DeleteWithdrawAsync(Guid withdrawId)
    {
        var withdrawResult = await GetWithdrawAsync(withdrawId);
        if (withdrawResult.IsFailed) return false;

        dataContext.Withdraws.Remove(withdrawResult.Value);
        await dataContext.SaveChangesAsync();
        return true;
    }

    public async Task<Result<Withdraw>> UpdateWithdrawStatusAsync(Guid withdrawId, Action<Withdraw> update)
    {
        var withdrawResult = await GetWithdrawAsync(withdrawId);
        if (withdrawResult.IsFailed) return withdrawResult.Exception;

        var withdraw = withdrawResult.Value;
        update(withdraw);
        await dataContext.SaveChangesAsync();
        return withdraw;
    }
}