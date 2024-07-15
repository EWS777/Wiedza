using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Wiedza.Core.Models.Data;

namespace Wiedza.Api.Data.Models;

[PrimaryKey(nameof(PersonId))]
public class PersonSalt
{
    [ForeignKey(nameof(Person))]public Guid PersonId { get; set; }
    public Person Person { get; set; }
    public string Salt { get; set; }
}