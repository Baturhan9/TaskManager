using System;
using System.Collections.Generic;

namespace TaskManager.Models;

public partial class Project
{
    public int ProjectId { get; set; }

    public string ShortName { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? DateOfCreate { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<UserAccess> UserAccesses { get; set; } = new List<UserAccess>();
}
