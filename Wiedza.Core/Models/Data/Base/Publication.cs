using System.Text.Json.Serialization;
using Wiedza.Core.Models.Enums;

namespace Wiedza.Core.Models.Data.Base;

public abstract class Publication
{
    public ulong Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public PublicationStatus Status { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }

    public Category? Category { get; set; }
    public Guid? CategoryId { get; set; }
    public Person Author { get; set; }
    public Guid AuthorId { get; set; }

    [JsonIgnore] public Type PublicationType { get; }
}