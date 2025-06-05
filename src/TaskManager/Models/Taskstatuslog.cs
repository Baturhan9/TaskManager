using System;
using System.Collections.Generic;

namespace TaskManager.Models;

public partial class TaskStatusLog
{
    public int TaskStatusId { get; set; }

    public int? TaskId { get; set; }

    public int? UserId { get; set; }

    public string? Comment { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? DateUpdate { get; set; } = DateTime.UtcNow;

    public virtual Task? Task { get; set; }

    public virtual User? User { get; set; }
}
