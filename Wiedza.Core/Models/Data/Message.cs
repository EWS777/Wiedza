namespace Wiedza.Core.Models.Data;

public class Message
{
    public Guid Id { get; set; }
    public DateTimeOffset SendedAt { get; set; }
    public DateTimeOffset? ReadedAt { get; set; }

    public Chat Chat { get; set; }
    public Guid ChatId { get; set; }
    public Person? Author { get; set; }
    public Guid? AuthorId { get; set; }
}