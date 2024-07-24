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
    IPersonRepository personRepository
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
        return await withdrawRepository.GetWithdrawAsync(withdrawId);
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

        // TODO Money logic

        return await withdrawRepository.AddWithdrawAsync(new Withdraw
        {
            Amount = amount,
            CardNumber = cardNumber,
            PersonId = personId
        });
    }

    public async Task<Result<Withdraw[]>> GetWithdrawsAsync(Guid userId)
    {
        throw new NotImplementedException();
        //if (!personResult.IsFailed) return await withdrawRepository.GetWithdrawsAsync(userId);
        //return await withdrawRepository.GetAdminWithdrawsAsync();
    }

    public async Task<Result<bool>> DeleteWithdrawAsync(Guid userId, Guid withdrawId)
    {
        var withdrawResult = await withdrawRepository.GetWithdrawAsync(withdrawId);
        if (withdrawResult.IsFailed) return withdrawResult.Exception;

        if (withdrawResult.Value.PersonId != userId) return new ForbiddenException("You are not an owner of this withdraw!");

        if (withdrawResult.Value.Status == WithdrawStatus.Completed)
            return new Exception("Your withdraw request was 'Completed' yet! You can't delete this withdraw");

        return await withdrawRepository.DeleteWithdrawAsync(withdrawId);
    }

    public async Task<Result<Withdraw>> UpdateWithdrawStatusAsync(Guid userId, Guid withdrawId, bool isCompleted)
    {

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