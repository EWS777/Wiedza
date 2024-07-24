using System.Text.Json.Serialization;
using Wiedza.Core.Attributes;
using Wiedza.Core.Models.Enums;

namespace Wiedza.Core.Models.Data.Base;

public abstract class Complaint
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
    public Administrator? Administrator { get; set; }
    public Guid? AdministratorId { get; set; }

    public ComplaintType ComplaintType { get; }
}

public enum ComplaintType
{
    [EnumTypeValue(typeof(PublicationComplaint))]
    PublicationComplaint,
    [EnumTypeValue(typeof(PersonComplaint))]
    PersonComplaint
}