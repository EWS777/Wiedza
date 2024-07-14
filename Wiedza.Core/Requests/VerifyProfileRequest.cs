namespace Wiedza.Core.Requests;

public class VerifyProfileRequest
{
    public ulong Pesel { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public byte[] ImageDocumentByte { get; set; }
}