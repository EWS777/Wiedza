using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Wiedza.Core.Models.Data.Base;

namespace Wiedza.Api.Data.Models;

[PrimaryKey(nameof(UserId))]
public class UserSalt
{
    [ForeignKey(nameof(User))]public Guid UserId { get; set; }
    public User User { get; set; }
    public string Salt { get; set; }
}