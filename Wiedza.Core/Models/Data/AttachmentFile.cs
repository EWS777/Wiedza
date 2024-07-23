namespace Wiedza.Core.Models.Data;

public class AttachmentFile
{
    public Guid Id { get; set; }
    public Guid personId { get; set; }
    public DateTimeOffset UploadedAt { get; set; }
    public byte[] FileBytes { get; set; }
}