using Wiedza.Core.Models.Data;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Repositories;

public interface IServiceRepository
{
    Task<Service[]> GetServicesAsync(bool onlyActive = true);
    Task<Service[]> GetPersonServicesAsync(Guid personId);
    Task<Result<Service>> GetServiceAsync(ulong serviceId);
    Task<Service> AddServiceAsync(Service service);
    Task<Result<Service>> UpdateServiceAsync(ulong serviceId, Action<Service> updateAction);
    Task<bool> DeleteServiceAsync(ulong serviceId);
}