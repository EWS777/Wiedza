﻿namespace Wiedza.Core.Models;

public class Chat
{
    public Guid Id { get; set; }
    public Offer Offer { get; set; }
    public Guid OfferId { get; set; }
}