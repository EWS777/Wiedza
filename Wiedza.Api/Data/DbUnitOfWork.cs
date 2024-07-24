using Microsoft.EntityFrameworkCore.Storage;

namespace Wiedza.Api.Data;

public class DbUnitOfWork(DataContext dataContext)
{
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await dataContext.Database.BeginTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await dataContext.Database.RollbackTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await dataContext.Database.CommitTransactionAsync();
    }
}