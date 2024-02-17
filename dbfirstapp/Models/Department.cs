using System;
using System.Collections.Generic;

namespace dbfirstapp.Models;

public partial class Department
{
    public int DId { get; set; }

    public string DName { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
