using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories.Implementations;

public class DbVerificationRepository(DataContext dataContext) : IVerificationRepository
{
    public async Task<Verification[]> GetAllVerifications()
    {
        return await dataContext.Verifications
            .Include(x => x.Person)
            .Include(x => x.Administrator)
            .AsNoTracking().ToArrayAsync();
    }

    public async Task<Result<Verification>> GetVerificationAsync(Guid verificationId)
    {
        var verification = await dataContext.Verifications
            .Include(x => x.Person)
            .Include(x => x.Administrator)
            .SingleOrDefaultAsync(x => x.Id == verificationId);
        if (verification is null) return new VerificationNotFoundException(verificationId);
        return verification;
    }

    public async Task<Verification> AddVerificationAsync(Verification verification)
    {
        await dataContext.Verifications.AddAsync(verification);
        await dataContext.SaveChangesAsync();
        return verification;
    }

    public async Task<Result<Verification>> UpdateVerificationStatusAsync(Guid verificationId, Action<Verification> update)
    {
        var verificationResult = await GetVerificationAsync(verificationId);
        if (verificationResult.IsFailed) return verificationResult.Exception;

        var verification = verificationResult.Value;
        update(verification);
        await dataContext.SaveChangesAsync();
        return verification;
    }

    public async Task<Result<bool>> DeleteVerificationAsync(Guid verificationId)
    {
        var verificationResult = await GetVerificationAsync(verificationId);
        if (verificationResult.IsFailed) return false;

        dataContext.Verifications.Remove(verificationResult.Value);
        await dataContext.SaveChangesAsync();
        return true;
    }
}