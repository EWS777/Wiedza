using Wiedza.Core.Models.Data;

namespace Wiedza.Core.Services;

public interface IStatisticService
{
    Task<WebsiteBalance> GetBalanceAsync();
}