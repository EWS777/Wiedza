using Wiedza.Core.Models.Enums;

namespace Wiedza.Core.Models;

public class Withdraw
{
    public Guid Id { get; set; }
    public float Amount { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? WithdrawTime { get; set; }
    public WithdrawStatus Status { get; set; }
    public ulong CardNumber { get; set; }
    public Person Person { get; set; }
    public Guid PersonId { get; set; }
}