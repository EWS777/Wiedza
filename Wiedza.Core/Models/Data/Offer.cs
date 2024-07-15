using Wiedza.Core.Models.Enums;

namespace Wiedza.Core.Models.Data;

public class Offer
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public OfferStatus Status { get; set; }
    public string? Message { get; set; }
    public float CompanyProfit { get; set; }
    public float FreelancerProfit { get; set; }

    public Publication? Publication { get; set; }
    public ulong? PulicationId { get; set; }

    public Person Person { get; set; }
    public Guid PersonId { get; set; }
}