namespace Wiedza.Core.Models.Enums;

public enum OfferStatus
{
    /// <summary>
    /// Rejected - on start when the client don't choose this user
    /// Canceled - decide cancel this user while performing task 
    /// </summary>
    New,
    Approved,
    Rejected,
    Completed,
    Canceled
}