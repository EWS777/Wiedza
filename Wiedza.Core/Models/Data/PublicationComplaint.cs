using Wiedza.Core.Models.Data.Base;
using Wiedza.Core.Models.Enums;

namespace Wiedza.Core.Models.Data;

public class PublicationComplaint : Complaint
{
    public Publication Publication { get; set; }
    public ulong PublicationId { get; set; }
}