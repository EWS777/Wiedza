using Wiedza.Core.Models.Enums;

namespace Wiedza.Core.Models.Complaints;

public class PersonComplaint
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ComplaintStatus Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? FinishedAt { get; set; }
    public AttachmentFile AttachmentFile { get; set; }
    public Guid AttachmentFileId { get; set; }
    public Person Author { get; set; }
    public Guid AuthorId { get; set; }
    public Person Person { get; set; }
    public Guid PersonId { get; set; }
    public Administrator? Administrator { get; set; }
    public Guid? AdministratorId { get; set; }
}