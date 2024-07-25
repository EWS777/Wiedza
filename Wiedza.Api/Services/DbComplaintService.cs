using Wiedza.Api.Repositories;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Data.Base;
using Wiedza.Core.Models.Enums;
using Wiedza.Core.Requests;
using Wiedza.Core.Services;
using Wiedza.Core.Utilities;

namespace Wiedza.Api.Services;

public class DbComplaintService(
    IComplaintRepository complaintRepository,
    IPersonRepository personRepository,
    IPublicationRepository publicationRepository
) : IComplaintService
{
    public async Task<PersonComplaint[]> GetPersonComplaintsAsync()
    {
        return await complaintRepository.GetPersonComplaintsAsync();
    }

    public async Task<PersonComplaint[]> GetPersonComplaintsAsync(Guid personId)
    {
        return await complaintRepository.GetPersonComplaintsAsync(personId);
    }

    public async Task<PublicationComplaint[]> GetPublicationComplaintsAsync()
    {
        return await complaintRepository.GetPublicationComplaintsAsync();
    }

    public async Task<PublicationComplaint[]> GetPublicationComplaintsAsync(ulong publicationId)
    {
        return await complaintRepository.GetPublicationComplaintsAsync(publicationId);
    }

    public async Task<Result<PersonComplaint>> GetPersonComplaintAsync(Guid personComplaintId)
    {
        return await complaintRepository.GetPersonComplaintAsync(personComplaintId);
    }

    public async Task<Result<PublicationComplaint>> GetPublicationComplaintAsync(Guid publicationComplaintId)
    {
        return await complaintRepository.GetPublicationComplaintAsync(publicationComplaintId);
    }

    public async Task<Result<PersonComplaint>> AddPersonComplaintAsync(string username, Guid authorId,
        AddComplaintRequest request)
    {
        var personResult = await personRepository.GetPersonAsync(username);
        if (personResult.IsFailed) return personResult.Exception;

        var person = personResult.Value;

        var complaint = new PersonComplaint
        {
            AuthorId = authorId,
            PersonId = person.Id,
            Title = request.Title,
            Description = request.Description
        };
        if (request.FileBytes is not null)
            complaint.AttachmentFile = new AttachmentFile
            {
                FileBytes = request.FileBytes, PersonId = authorId
            };

        return await complaintRepository.AddPersonComplaintAsync(complaint);
    }

    public async Task<Result<PublicationComplaint>> AddPublicationComplaintAsync(ulong publicationId, Guid authorId,
        AddComplaintRequest request)
    {
        var publicationResult = await publicationRepository.GetPublicationAsync(publicationId);
        if (publicationResult.IsFailed) return publicationResult.Exception;

        var complaint = new PublicationComplaint
        {
            AuthorId = authorId,
            PublicationId = publicationId,
            Title = request.Title,
            Description = request.Description
        };
        if (request.FileBytes is not null)
            complaint.AttachmentFile = new AttachmentFile
            {
                FileBytes = request.FileBytes, PersonId = authorId
            };

        return await complaintRepository.AddPublicationComplaintAsync(complaint);
    }

    public async Task<Result<Complaint>> UpdateComplaintAsync(Guid complaintId, Guid adminId, bool isCompleted)
    {
        return await complaintRepository.UpdateComplaintAsync(complaintId, complaint =>
        {
            complaint.Status = isCompleted ? ComplaintStatus.Completed : ComplaintStatus.Rejected;
            complaint.AdministratorId = adminId;
        });
    }
}