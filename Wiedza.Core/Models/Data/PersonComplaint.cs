using Wiedza.Core.Models.Data.Base;

namespace Wiedza.Core.Models.Data;

public class PersonComplaint : Complaint
{
    public Person Person { get; set; }
    public Guid PersonId { get; set; }
}