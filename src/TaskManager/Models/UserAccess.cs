using System;
using System.Collections.Generic;

namespace TaskManager.Models;

public partial class UserAccess
{
    public int UserAccessId { get; set; }

    public int? UserId { get; set; }

    public int? ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public virtual User? User { get; set; }
}
