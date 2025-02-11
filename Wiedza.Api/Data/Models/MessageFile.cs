﻿using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Wiedza.Core.Models.Data;

namespace Wiedza.Api.Data.Models;

[PrimaryKey(nameof(MessageId), nameof(AttachmentFileId))]
public class MessageFile
{
    public Message Message { get; set; }
    [ForeignKey(nameof(Message))] public Guid MessageId { get; set; }
    public AttachmentFile AttachmentFile { get; set; }
    [ForeignKey(nameof(AttachmentFile))] public Guid AttachmentFileId { get; set; }
}