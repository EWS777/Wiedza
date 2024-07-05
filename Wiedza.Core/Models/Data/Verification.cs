namespace Wiedza.Core.Models.Data;

public class Verification
{
    public Guid Id { get; set; }
    public ulong Pesel { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? VerificationTime { get; set; }
    public bool IsVerificated { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public byte[] ImageDocumentBytes { get; set; }
    public Person Person { get; set; }
    public Guid PersonId { get; set; }
}