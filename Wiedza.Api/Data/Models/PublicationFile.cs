using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Wiedza.Core.Models.Data;
using Wiedza.Core.Models.Data.Base;

namespace Wiedza.Api.Data.Models;

[PrimaryKey(nameof(PublicationId), nameof(AttachmentFileId))]
public class PublicationFile
{
    public PublicationBase PublicationBase { get; set; }
    [ForeignKey(nameof(PublicationBase))] public ulong PublicationId { get; set; }
    public AttachmentFile AttachmentFile { get; set; }
    [ForeignKey(nameof(AttachmentFile))] public Guid AttachmentFileId { get; set; }
}