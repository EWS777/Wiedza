namespace Wiedza.Core.Models.Enums;

public enum OfferStatus
{
    /// <summary>
    /// Rejected - on start when the client don't choose this person
    /// Canceled - decide cancel this person while performing task 
    /// </summary>
    New,
    Approved,
    Rejected,
    Completed,
    Canceled
}