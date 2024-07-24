using Microsoft.EntityFrameworkCore;
using Wiedza.Api.Data;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories.Implementations;

public class DbServiceRepository(DataContext dataContext) : IServiceRepository
{
    public async Task<Service[]> GetServicesAsync(bool onlyActive = true)
    {
        var services = dataContext.Services
            .Include(p => p.Author)
            .Include(p => p.Category)
            .AsNoTracking();

        if (onlyActive) services = services.Where(p => p.Status == PublicationStatus.Active);

        return await services.ToArrayAsync();
    }

    public async Task<Service[]> GetPersonServicesAsync(Guid personId)
    {
        return await dataContext.Services
            .Include(p => p.Author)
            .Include(p => p.Category)
            .Where(p => p.AuthorId == personId)
            .AsNoTracking().ToArrayAsync();
    }

    public async Task<Result<Service>> GetServiceAsync(ulong serviceId)
    {
        var service = await dataContext.Services.SingleOrDefaultAsync(p => p.Id == serviceId);
        if (service is null) return new ServiceNotFoundException(serviceId);

        return service;
    }

    public async Task<Service> AddServiceAsync(Service service)
    {
        await dataContext.Services.AddAsync(service);
        await dataContext.SaveChangesAsync();
        return service;
    }

    public async Task<Result<Service>> UpdateServiceAsync(ulong serviceId, Action<Service> updateAction)
    {
        var serviceResult = await GetServiceAsync(serviceId);
        if (serviceResult.IsFailed) return serviceResult.Exception;

        var service = serviceResult.Value;
        updateAction(service);
        await dataContext.SaveChangesAsync();
        return service;
    }

    public async Task<bool> DeleteServiceAsync(ulong serviceId)
    {
        var serviceResult = await GetServiceAsync(serviceId);
        if (serviceResult.IsFailed) return false;

        var service = serviceResult.Value;
        dataContext.Services.Remove(service);
        await dataContext.SaveChangesAsync();
        return true;
    }
}