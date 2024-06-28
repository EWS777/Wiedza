namespace Wiedza.Core.Models;

public class Payment
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public float Amount { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public uint House { get; set; }
    public uint PostalCode { get; set; }
    public ulong CardNumber { get; set; }
    public string Email { get; set; }
    public float Commission { get; set; }
    public float ReceivedAmount { get; set; }
    public Person Person { get; set; }
    public Guid PersonId { get; set; }
}