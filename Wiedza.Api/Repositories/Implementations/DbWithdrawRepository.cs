using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories.Implementations;

public class DbWithdrawRepository(DataContext dataContext) : IWithdrawRepository
{
    public async Task<Withdraw[]> GetAllWithdrawsAsync()
    {
        return await dataContext.Withdraws
            .Include(p => p.Person)
            .Include(p => p.Administrator)
            .AsNoTracking().ToArrayAsync();
    }

    public async Task<Withdraw[]> GetPersonWithdrawsAsync(Guid personId)
    {
        return await dataContext.Withdraws
            .Include(p => p.Person)
            .Where(x=>x.Id == personId)
            .AsNoTracking().ToArrayAsync();
    }

    public async Task<Result<Withdraw>> GetWithdrawAsync(Guid withdrawId)
    {
        var withdraw = await dataContext.Withdraws
            .Include(x => x.Person)
            .Include(x => x.Administrator)
            .SingleOrDefaultAsync(x => x.Id == withdrawId);
        if (withdraw is null) return new WithdrawNotFoundException(withdrawId);

        return withdraw;
    }

    public async Task<Withdraw> AddWithdrawAsync(Withdraw withdraw)
    {
        await dataContext.Withdraws.AddAsync(withdraw);
        await dataContext.SaveChangesAsync();
        return withdraw;
    }

    public async Task<Result<Withdraw>> UpdateWithdrawAsync(Guid withdrawId, Action<Withdraw> update)
    {
        var withdrawResult = await GetWithdrawAsync(withdrawId);
        if (withdrawResult.IsFailed) return withdrawResult.Exception;

        var withdraw = withdrawResult.Value;
        update(withdraw);
        await dataContext.SaveChangesAsync();
        return withdraw;
    }

    public async Task<bool> DeleteWithdrawAsync(Guid withdrawId)
    {
        var withdrawResult = await GetWithdrawAsync(withdrawId);
        if (withdrawResult.IsFailed) return false;

        dataContext.Withdraws.Remove(withdrawResult.Value);
        await dataContext.SaveChangesAsync();
        return true;
    }
}