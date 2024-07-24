using Wiedza.Api.Repositories;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Services;

public class DbWithdrawService(
    IWithdrawRepository withdrawRepository,
    IPersonRepository personRepository
    ) : IWithdrawService
{
    public async Task<Result<Withdraw>> GetWithdrawAsync(Guid userId, Guid withdrawId)
    {
        var withdrawResult = await withdrawRepository.GetWithdrawAsync(withdrawId);
        if (withdrawResult.IsFailed) return withdrawResult.Exception;
        
        var withdraw = withdrawResult.Value;
        
        var adminResult = await personRepository.GetAdministratorAsync(userId);

        if (withdraw.PersonId != userId && adminResult.IsFailed)
            return new ForbiddenException("You don't have permission to get this withdraw!");
        return withdraw;
    }

    public async Task<Result<Withdraw>> AddWithdrawAsync(Guid userId, float amount, ulong cardNumber)
    {
        var personResult = await personRepository.GetPersonAsync(userId);
        if (personResult.IsFailed) return personResult.Exception;

        var person = personResult.Value;

        if (!person.IsVerificated) return new ForbiddenException("You are not verified!");

        if (person.Balance < 30.0f)
            return new NotEnoughMoneyException("You can't withdraw money! Your balance should be greater than 30 PLN!");
        
        if(person.Balance < amount) return new NotEnoughMoneyException("You don't have enough money!");

        return await withdrawRepository.AddWithdrawAsync(new Withdraw
        {
            Amount = amount,
            CardNumber = cardNumber,
            PersonId = userId
        });
    }

    public async Task<Result<Withdraw[]>> GetWithdrawsAsync(Guid userId)
    {
        var personResult = await personRepository.GetPersonAsync(userId);
        var adminResult = await personRepository.GetAdministratorAsync(userId);
        if (personResult.IsFailed && adminResult.IsFailed)
            return new ForbiddenException("You don't have permission to get withdraws!");


        if (!personResult.IsFailed) return await withdrawRepository.GetWithdrawsAsync(userId);
        return await withdrawRepository.GetAdminWithdrawsAsync();
    }

    public async Task<Result<bool>> DeleteWithdrawAsync(Guid userId, Guid withdrawId)
    {
        var withdrawResult = await withdrawRepository.GetWithdrawAsync(withdrawId);
        if (withdrawResult.IsFailed) return withdrawResult.Exception;
        
        if(withdrawResult.Value.PersonId != userId) return new ForbiddenException("You are not an owner of this withdraw!");

        if (withdrawResult.Value.Status == WithdrawStatus.Completed)
            return new Exception("Your withdraw request was 'Completed' yet! You can't delete this withdraw");

        return await withdrawRepository.DeleteWithdrawAsync(withdrawId);
    }

    public async Task<Result<Withdraw>> UpdateWithdrawStatusAsync(Guid userId, Guid withdrawId, bool isCompleted)
    {
        var adminResult = await personRepository.GetAdministratorAsync(userId);
        if (adminResult.IsFailed) return new ForbiddenException("You don't have permission to change withdraw status!");
        
        var withdrawResult = await withdrawRepository.GetWithdrawAsync(withdrawId);
        if (withdrawResult.IsFailed) return withdrawResult.Exception;
        
        if (withdrawResult.Value.Person.Balance - withdrawResult.Value.Amount < 0.0f)
            return new NotEnoughMoneyException("Not enough money on balance!");
            
        if (isCompleted)
        {
            await personRepository.UpdatePersonAsync(userId, person =>
            {
                person.Balance -= withdrawResult.Value.Amount;
            });
        }

        return await withdrawRepository.UpdateWithdrawStatusAsync(withdrawId, withdrawUpdate =>
        {
            withdrawUpdate.Status = isCompleted ? WithdrawStatus.Completed : WithdrawStatus.Rejected;
            withdrawUpdate.AdministratorId = userId;
        });
    }
}