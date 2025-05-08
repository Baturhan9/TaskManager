using System;
using System.Collections.Generic;

namespace TaskManager.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? Deadline { get; set; }

    public int? AuthorId { get; set; }

    public int? ReviewerId { get; set; }

    public int? TesterId { get; set; }

    public int? AssignmentId { get; set; }

    public int? ProjectId { get; set; }

    public virtual User? Assignment { get; set; }

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    public virtual User? Author { get; set; }

    public virtual Project? Project { get; set; }

    public virtual User? Reviewer { get; set; }

    public virtual ICollection<TaskStatusLog> TaskStatusLogs { get; set; } =
        new List<TaskStatusLog>();

    public virtual User? Tester { get; set; }
}
