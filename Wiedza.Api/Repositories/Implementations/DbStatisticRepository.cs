using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data;
using Wiedza.Core.Models.Data;

namespace Wiedza.Api.Repositories.Implementations;

public class DbStatisticRepository(DataContext dataContext) : IStatisticRepository
{
    public async Task<WebsiteBalance> GetBalanceAsync()
    {
        var balance = await dataContext.WebsiteBalances.SingleOrDefaultAsync(x => x.Id == 1);
        return balance!;
    }

    public async Task AddIncomeBalanceAsync(float amount)
    {
        var balance =  await dataContext.WebsiteBalances.SingleOrDefaultAsync(x => x.Id == 1);
        balance!.NetIncome += amount;
        await dataContext.SaveChangesAsync();
    }
}