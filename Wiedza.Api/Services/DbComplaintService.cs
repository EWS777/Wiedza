using Wiedza.Api.Repositories;
using Wiedza.Core.Exceptions;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;
using Wiedza.Core.Requests;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Services;

public class DbComplaintService(
    IComplaintRepository complaintRepository,
    IPersonRepository personRepository) : IComplaintService
{
    public async Task<PersonComplaint> AddPersonComplaintAsync(Guid authorId, Guid personId, AddComplaintRequest addComplaintRequest)
    {
        return await complaintRepository.AddPersonComplaintAsync(new PersonComplaint
        {
            AuthorId = authorId,
            PersonId = personId,
            Title = addComplaintRequest.Title,
            Description = addComplaintRequest.Description,
            AttachmentFile = new AttachmentFile
            {
                FileBytes = addComplaintRequest.FileBytes!,
                personId = authorId
            }
        });
    }

    public async Task<PublicationComplaint> AddPublicationComplaintAsync(Guid authorId, ulong publicationId, AddComplaintRequest addComplaintRequest)
    {
        return await complaintRepository.AddPublicationComplaintAsync(new PublicationComplaint
        {
            AuthorId = authorId,
            PublicationId = publicationId,
            Title = addComplaintRequest.Title,
            Description = addComplaintRequest.Description,
            AttachmentFile = new AttachmentFile
            {
                FileBytes = addComplaintRequest.FileBytes!,
                personId = authorId
            }
        });
    }

    public async Task<PersonComplaint[]> GetPersonComplaintsAsync()
    {
        return await complaintRepository.GetPersonComplaintsAsync();
    }

    public async Task<PublicationComplaint[]> GetPublicationComplaintsAsync()
    {
        return await complaintRepository.GetPublicationComplaintsAsync();
    }

    public async Task<Result<PersonComplaint>> GetPersonComplaintAsync(Guid personComplaintId)
    {
        return await complaintRepository.GetPersonComplaintAsync(personComplaintId);
    }

    public async Task<Result<PublicationComplaint>> GetPublicationComplaintAsync(Guid publicationComplaintId)
    {
        return await complaintRepository.GetPublicationComplaintAsync(publicationComplaintId);
    }

    public async Task<Result<PersonComplaint>> ModifyPersonComplaintAsync(Guid adminId, Guid personComplaintId, bool isCompleted)
    {
        var adminResult = await personRepository.GetAdministratorAsync(adminId);
        if (adminResult.IsFailed) return new ForbiddenException("You are not an owner!");

        var complaintResult = await complaintRepository.GetPersonComplaintAsync(personComplaintId);
        if (complaintResult.IsFailed) return new ComplaintNotFoundException(personComplaintId);

        if (isCompleted)
        {
            complaintResult.Value.FinishedAt = DateTimeOffset.Now;
        }
        
        return await complaintRepository.ModifyPersonComplaintAsync(personComplaintId, (complaintModify =>
        {
            complaintModify.AdministratorId = adminId;
            complaintModify.Status = isCompleted ? ComplaintStatus.Completed : ComplaintStatus.Rejected;
        }));
    }

    public async Task<Result<PublicationComplaint>> ModifyPublicationComplaintAsync(Guid adminId, Guid publicationComplaintId, bool isCompleted)
    {
        var adminResult = await personRepository.GetAdministratorAsync(adminId);
        if (adminResult.IsFailed) return new ForbiddenException("You are not an owner!");

        var complaintResult = await complaintRepository.GetPublicationComplaintAsync(publicationComplaintId);
        if (complaintResult.IsFailed) return new ComplaintNotFoundException(publicationComplaintId);

        if (isCompleted)
        {
            complaintResult.Value.FinishAt = DateTimeOffset.Now;
        }
        
        return await complaintRepository.ModifyPublicationComplaintAsync(publicationComplaintId, (complaintModify =>
        {
            complaintModify.AdministratorId = adminId;
            complaintModify.Status = isCompleted ? ComplaintStatus.Completed : ComplaintStatus.Rejected;
        }));
    }
}