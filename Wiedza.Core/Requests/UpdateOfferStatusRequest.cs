using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Enums;

namespace Wiedza.Core.Requests;

public class UpdateOfferStatusRequest
{
    public UpdateOfferStatus Status { get; set; }


    public UpdateOfferStatusRequest(Offer offer)
    {
        Status = offer.Status switch
        {
            OfferStatus.Approved => UpdateOfferStatus.Approved,
            OfferStatus.Canceled => UpdateOfferStatus.Canceled,
            OfferStatus.Completed => UpdateOfferStatus.Completed,
            OfferStatus.Rejected => UpdateOfferStatus.Rejected,
            _ => UpdateOfferStatus.Other
        };
    }
}

public enum UpdateOfferStatus
{
    Approved,
    Rejected,
    Completed,
    Canceled,
    Other
}