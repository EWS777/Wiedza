using Wiedza.Api.Core.Extensions;
using Wiedza.Api.Repositories;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;
using Wiedza.Core.Requests;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Services;

public class DbServiceService(IServiceRepository serviceRepository) : IServiceService
{
    public async Task<Service[]> GetServicesAsync(bool onlyActive = true)
    {
        return await serviceRepository.GetServicesAsync(onlyActive);
    }

    public async Task<Service[]> GetPersonServicesAsync(Guid personId)
    {
        return await serviceRepository.GetPersonServicesAsync(personId);
    }

    public async Task<Result<Service>> GetServiceAsync(ulong serviceId)
    {
        return await serviceRepository.GetServiceAsync(serviceId);
    }

    public async Task<Service> AddServiceAsync(Guid userId, AddPublicationRequest addPublicationRequest)
    {
        return await serviceRepository.AddServiceAsync(new Service
        {
            Title = addPublicationRequest.Title,
            Description = addPublicationRequest.Description,
            Price = addPublicationRequest.Price,
            CategoryId = addPublicationRequest.CategoryId,
            AuthorId = userId
        });
    }

    public async Task<Result<Service>> UpdateService(Guid personId, ulong serviceId, Action<UpdatePublicationRequest> update)
    {
        var serviceResult = await serviceRepository.GetServiceAsync(serviceId);
        if (serviceResult.IsFailed) return serviceResult.Exception;

        var service = serviceResult.Value;
        if (service.AuthorId != personId) return new ForbiddenException("You are not an owner of this service!");

        var request = new UpdatePublicationRequest(service);
        update(request);
        if (request.IsValidationFailed(out var exception)) return exception;

        return await serviceRepository.UpdateServiceAsync(serviceId, serviceUpdate =>
        {
            serviceUpdate.Title = request.Title;
            serviceUpdate.Description = request.Description;
            serviceUpdate.Price = request.Price;
            serviceUpdate.CategoryId = request.CategoryId;

            if (request.Status != PublicationUpdateStatus.Other)
            {
                serviceUpdate.Status = request.Status switch
                {
                    PublicationUpdateStatus.Active => PublicationStatus.Active,
                    PublicationUpdateStatus.Inactive => PublicationStatus.Inactive,
                    _ => throw new ArgumentOutOfRangeException(nameof(request.Status))
                };
            }
        });
    }

    public async Task<Result<bool>> DeleteServiceAsync(Guid personId, ulong serviceId)
    {
        var serviceResult = await serviceRepository.GetServiceAsync(serviceId);
        if (serviceResult.IsFailed) return serviceResult.Exception;

        var service = serviceResult.Value;

        if (service.AuthorId != personId) return new ForbiddenException("You are not an owner of this service!");

        return await serviceRepository.DeleteServiceAsync(serviceId);
    }
}