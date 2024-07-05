using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Wiedza.Core.Models.Data;

namespace Wiedza.Api.Data.Models;

[PrimaryKey(nameof(PublicationId), nameof(AttachmentFileId))]
public class PublicationFile
{
    public Publication Publication { get; set; }
    [ForeignKey(nameof(Publication))] public Guid PublicationId { get; set; }
    public AttachmentFile AttachmentFile { get; set; }
    [ForeignKey(nameof(AttachmentFile))] public Guid AttachmentFileId { get; set; }
}