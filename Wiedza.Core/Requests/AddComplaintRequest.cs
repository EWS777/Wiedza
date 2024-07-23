namespace Wiedza.Core.Requests;

public class AddComplaintRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public byte[]? FileBytes { get; set; }
    
}