using System;
using System.Collections.Generic;

namespace TaskManager.Models;

public partial class Attachment
{
    public int AttachmentId { get; set; }

    public int? TaskId { get; set; }

    public string FilePath { get; set; } = null!;

    public virtual Task? Task { get; set; }
}
