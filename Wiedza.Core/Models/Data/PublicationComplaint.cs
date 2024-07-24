using Wiedza.Core.Models.Data.Base;

namespace Wiedza.Core.Models.Data;

public class PublicationComplaint : Complaint
{
    public Publication Publication { get; set; }
    public ulong PublicationId { get; set; }
}