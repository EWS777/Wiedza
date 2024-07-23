using System.ComponentModel.DataAnnotations;

namespace Wiedza.Core.Requests;

public class AddReviewRequest
{
    public string? Message { get; set; }
    [Range(0,5)] public float Rating { get; set; }
}