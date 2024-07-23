using Microsoft.AspNetCore.Mvc;
using Wiedza.Core.Models.Data;

namespace Wiedza.Api.Repositories;

public interface IStatisticRepository
{
    Task<WebsiteBalance> GetBalanceAsync();
    Task AddIncomeBalanceAsync(float amount);
}