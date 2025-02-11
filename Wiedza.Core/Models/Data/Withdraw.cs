﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Wiedza.Core.Models.Enums;

namespace Wiedza.Core.Models.Data;

public class Withdraw
{
    public Guid Id { get; set; }
    public float Amount { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? WithdrawTime { get; set; }
    public WithdrawStatus Status { get; set; }
    [CreditCard] public string CardNumber { get; set; }
    public Person Person { get; set; }
    public Guid PersonId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Administrator? Administrator { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? AdministratorId { get; set; }
}