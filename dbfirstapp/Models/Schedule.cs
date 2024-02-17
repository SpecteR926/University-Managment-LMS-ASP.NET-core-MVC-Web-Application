using System;
using System.Collections.Generic;

namespace dbfirstapp.Models;

public partial class Schedule
{
    public int EId { get; set; }

    public DateOnly EDate { get; set; }

    public int? CourseId { get; set; }

    public int? DeptmentId { get; set; }

    public int? TeacherId { get; set; }

    public string? CourseName { get; set; }

    public string? RNumber { get; set; }

    public TimeOnly? ETime { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Course? CourseNameNavigation { get; set; }

    public virtual Department? Deptment { get; set; }

    public virtual Teacher? Teacher { get; set; }
}
