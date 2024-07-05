namespace Wiedza.Core.Models.Data;

public class Chat
{
    public Guid Id { get; set; }
    public Offer Offer { get; set; }
    public Guid OfferId { get; set; }
}