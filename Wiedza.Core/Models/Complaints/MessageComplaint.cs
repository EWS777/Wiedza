using Wiedza.Core.Models.Enums;

namespace Wiedza.Core.Models.Complaints;

public class MessageComplaint
{
    public Guid Id { get; set; }
    public ComplaintStatus Status { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? FinishAt { get; set; }
    public Person Author { get; set; }
    public Guid AuthorId { get; set; }
    public Message Message { get; set; }
    public Guid MessageId { get; set; }
    public Administrator? Administrator { get; set; }
    public Guid? AdministratorId { get; set; }
}