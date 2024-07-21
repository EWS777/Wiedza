using Wiedza.Core.Models.Data;
using Wiedza.Core.Requests;
using Wiedza.Core.Utilities;

namespace Wiedza.Core.Services;

public interface IServiceService
{
    Task<Service[]> GetServicesAsync(bool onlyActive = true);
    Task<Service[]> GetPersonServicesAsync(Guid personId);
    Task<Result<Service>> GetServiceAsync(ulong serviceId);
    Task<Service> AddServiceAsync(Guid userId, AddPublicationRequest addPublicationRequest);
    Task<Result<Service>> UpdateService(Guid personId, ulong serviceId, Action<UpdatePublicationRequest> update);
    Task<Result<bool>> DeleteServiceAsync(Guid personId, ulong serviceId);
}