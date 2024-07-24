using Wiedza.Core.Models.Data.Base;
using Wiedza.Core.Models.Enums;

namespace Wiedza.Core.Models.Data;

public class PersonComplaint : Complaint
{
    public Person Person { get; set; }
    public Guid PersonId { get; set; }
}