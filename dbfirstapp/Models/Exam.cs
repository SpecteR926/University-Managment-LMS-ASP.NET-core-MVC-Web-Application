using System;
using System.Collections.Generic;

namespace dbfirstapp.Models;

public partial class Exam
{
    public int EId { get; set; }

    public DateOnly EDate { get; set; }

    public int? CourseId { get; set; }

    public string? CourseName { get; set; }

    public string? RNumber { get; set; }

    public TimeOnly ETime { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Course? CourseNameNavigation { get; set; }
}
