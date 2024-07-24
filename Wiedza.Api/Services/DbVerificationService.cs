using Wiedza.Api.Data;
using Wiedza.Api.Repositories;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Data.Base;
using Wiedza.Core.Models.Enums;
using Wiedza.Core.Requests;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Services;

public class DbVerificationService(
    IVerificationRepository verificationRepository,
    IUserRepository userRepository,
    IPersonRepository personRepository,
    DbUnitOfWork dbUnitOfWork) : IVerificationService
{
    public async Task<Verification[]> GetAllVerificationsAsync()
    {
        return await verificationRepository.GetAllVerifications();
    }

    public async Task<Result<Verification>> GetVerificationAsync(Guid verificationId, Guid userId)
    {
        var userResult = await userRepository.GetUserAsync(userId);
        if (userResult.IsFailed) return userResult.Exception;

        var user = userResult.Value;
        var verificationResult = await verificationRepository.GetVerificationAsync(verificationId);
        if (verificationResult.IsFailed) return verificationResult.Exception;

        var verification = verificationResult.Value;

        if (user.UserType is UserType.Administrator) return verification;
        if (verification.PersonId != userId) return new ForbiddenException("You are not owner of the verification!");
        return verification;
    }

    public async Task<Verification> AddVerificationAsync(Guid userId, AddVerificationRequest request)
    {
        return await verificationRepository.AddVerificationAsync(new Verification
        {
            Pesel = request.Pesel,
            Name = request.Name,
            Surname = request.Surname,
            ImageDocumentBytes = request.ImageDocumentBytes,
            PersonId = userId
        });
    }

    public async Task<Result<Verification>> UpdateVerificationStatusAsync(Guid verificationId, Guid userId, bool isCompleted)
    {
        var verificationResult = await verificationRepository.GetVerificationAsync(verificationId);
        if (verificationResult.IsFailed) return verificationResult.Exception;
        
        await using var transaction = await dbUnitOfWork.BeginTransactionAsync();
        try
        {
            if (isCompleted)
            {
                var result = await personRepository.UpdatePersonAsync(verificationResult.Value.PersonId, update =>
                {
                    update.IsVerificated = true;
                });
                if (result.IsFailed) throw result.Exception;
            }
            
            var final = await verificationRepository.UpdateVerificationStatusAsync(verificationId, update =>
            {
                update.AdministratorId = userId;
                update.Status = isCompleted ? VerificationStatus.Completed : VerificationStatus.Cancelled;
            });
            if (final.IsFailed) throw final.Exception;
            await transaction.CommitAsync();
            return final.Value;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return e;
        }
        
    }

    public async Task<Result<bool>> DeleteVerificationAsync(Guid verificationId, Guid userId)
    {
        var verificationResult = await verificationRepository.GetVerificationAsync(verificationId);
        if (verificationResult.IsFailed) return verificationResult.Exception;

        var verification = verificationResult.Value;
        if (verification.PersonId != userId) return new ForbiddenException("You are not an owner of this verification!");

        if (verification.Status == VerificationStatus.Completed)
            return new Exception("Your verification request is 'Completed'. You can't delete this verification");

        return await verificationRepository.DeleteVerificationAsync(verificationId);
    }
}