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

public class DbWithdrawService(
    IWithdrawRepository withdrawRepository,
    IPersonRepository personRepository,
    IUserRepository userRepository,
    DbUnitOfWork dbUnitOfWork
) : IWithdrawService
{
    public async Task<Withdraw[]> GetAllWithdrawsAsync()
    {
        return await withdrawRepository.GetAllWithdrawsAsync();
    }

    public async Task<Withdraw[]> GetPersonWithdrawsAsync(Guid personId)
    {
        return await withdrawRepository.GetPersonWithdrawsAsync(personId);
    }

    public async Task<Result<Withdraw>> GetWithdrawAsync(Guid withdrawId, Guid userId)
    {
        var userResult = await userRepository.GetUserAsync(userId);
        if (userResult.IsFailed) return userResult.Exception;

        var user = userResult.Value;
        var withdrawResult = await withdrawRepository.GetWithdrawAsync(withdrawId);
        if (withdrawResult.IsFailed) return withdrawResult.Exception;

        var withdraw = withdrawResult.Value;

        if (user.UserType is UserType.Administrator) return withdraw;
        if (withdraw.PersonId != userId) return new ForbiddenException("You are not owner of the withdraw!");
        return withdraw;
    }

    public async Task<Result<Withdraw>> AddWithdrawAsync(Guid personId, AddWithdrawRequest request)
    {
        var personResult = await personRepository.GetPersonAsync(personId);
        if (personResult.IsFailed) return personResult.Exception;

        var person = personResult.Value;

        if (!person.IsVerificated) return new ForbiddenException("You are not verified!");

        var amount = request.Amount;
        var cardNumber = request.CardNumber;

        if (person.Balance < 30.0f)
            return new NotEnoughMoneyException("You can't withdraw money! Your balance should be greater than 30 PLN!");

        if (person.Balance < amount) return new NotEnoughMoneyException("You don't have enough money!");


        await using var transaction = await dbUnitOfWork.BeginTransactionAsync();
        try
        {
            var updateResult =
                await personRepository.UpdatePersonAsync(personId, updatePerson => { updatePerson.Balance -= amount; });
            if (updateResult.IsFailed) throw updateResult.Exception;

            var addWithdrawAsync = await withdrawRepository.AddWithdrawAsync(new Withdraw
            {
                Amount = amount,
                CardNumber = cardNumber,
                PersonId = personId
            });
            await transaction.CommitAsync();
            return addWithdrawAsync;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return e;
        }
    }


    public async Task<Result<Withdraw>> UpdateWithdrawStatusAsync(Guid withdrawId, Guid userId, bool isCompleted)
    {
        var withdrawResult = await withdrawRepository.GetWithdrawAsync(withdrawId);
        if (withdrawResult.IsFailed) return withdrawResult.Exception;

        await using var transaction = await dbUnitOfWork.BeginTransactionAsync();

        try
        {
            if (isCompleted is false)
            {
                var updatePersonResult = await personRepository.UpdatePersonAsync(userId,
                    person => { person.Balance += withdrawResult.Value.Amount; });
                if (updatePersonResult.IsFailed) throw updatePersonResult.Exception;
            }

            var updateWithdrawResult = await withdrawRepository.UpdateWithdrawAsync(withdrawId, withdrawUpdate =>
            {
                withdrawUpdate.Status = isCompleted ? WithdrawStatus.Completed : WithdrawStatus.Rejected;
                withdrawUpdate.AdministratorId = userId;
            });
            if (updateWithdrawResult.IsFailed) throw updateWithdrawResult.Exception;
            await transaction.CommitAsync();
            return updateWithdrawResult.Value;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return e;
        }
    }

    public async Task<Result<bool>> DeleteWithdrawAsync(Guid withdrawId, Guid userId)
    {
        var withdrawResult = await withdrawRepository.GetWithdrawAsync(withdrawId);
        if (withdrawResult.IsFailed) return withdrawResult.Exception;

        var withdraw = withdrawResult.Value;
        if (withdraw.PersonId != userId) return new ForbiddenException("You are not an owner of this withdraw!");

        if (withdraw.Status == WithdrawStatus.Completed)
            return new Exception("Your withdraw request was 'Completed'. You can't delete this withdraw");

        await using var transaction = await dbUnitOfWork.BeginTransactionAsync();

        try
        {
            var updateResult = await personRepository.UpdatePersonAsync(userId,
                updatePerson => { updatePerson.Balance += withdraw.Amount; });
            if (updateResult.IsFailed) throw updateResult.Exception;

            var deleteResult = await withdrawRepository.DeleteWithdrawAsync(withdrawId);
            await transaction.CommitAsync();
            return deleteResult;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return e;
        }
    }
}