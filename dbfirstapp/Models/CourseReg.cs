using System;
using System.Collections.Generic;

namespace dbfirstapp.Models;

public partial class CourseReg
{
    public int RId { get; set; }

    public string? RName { get; set; }

    public int RDept { get; set; }

    public int? RTeacher { get; set; }

    public string? RTeacherName { get; set; }

    public int? NoOfRegsStds { get; set; }

    public virtual Department RDeptNavigation { get; set; } = null!;

    public virtual Course RIdNavigation { get; set; } = null!;

    public virtual Course? RNameNavigation { get; set; }

    public virtual Teacher? RTeacherNavigation { get; set; }
}
