using Wiedza.Api.Repositories;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Services;

namespace Wiedza.Api.Services;

public class DbStatisticService(IStatisticRepository statisticRepository) : IStatisticService
{
    public async Task<WebsiteBalance> GetBalanceAsync()
    {
        return await statisticRepository.GetBalanceAsync();
    }
}