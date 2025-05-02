using System;
using System.Collections.Generic;

namespace TaskManager.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Task> TaskAssignments { get; set; } = new List<Task>();

    public virtual ICollection<Task> TaskAuthors { get; set; } = new List<Task>();

    public virtual ICollection<Task> TaskReviewers { get; set; } = new List<Task>();

    public virtual ICollection<Task> TaskTesters { get; set; } = new List<Task>();

    public virtual ICollection<TaskStatusLog> TaskStatusLogs { get; set; } = new List<TaskStatusLog>();

    public virtual ICollection<UserAccess> UserAccesses { get; set; } = new List<UserAccess>();
}
