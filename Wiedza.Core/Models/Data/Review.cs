﻿using System.ComponentModel.DataAnnotations;

namespace Wiedza.Core.Models.Data;

public class Review
{
    public Guid Id { get; set; }
    public string? Message { get; set; }

    [Range(0, 5)] public float Rating { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public Person Person { get; set; }
    public Guid PersonId { get; set; }
    public Person Author { get; set; }
    public Guid AuthorId { get; set; }
}