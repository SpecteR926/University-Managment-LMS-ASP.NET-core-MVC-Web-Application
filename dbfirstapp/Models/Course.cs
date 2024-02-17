using System;
using System.Collections.Generic;

namespace dbfirstapp.Models;

public partial class Course
{
    public int CId { get; set; }

    public string CName { get; set; } = null!;

    public int? DeptmentId { get; set; }

    public virtual Department? Deptment { get; set; }

    public virtual ICollection<Exam> ExamCourseNameNavigations { get; set; } = new List<Exam>();

    public virtual ICollection<Exam> ExamCourses { get; set; } = new List<Exam>();

    public virtual ICollection<Schedule> ScheduleCourseNameNavigations { get; set; } = new List<Schedule>();

    public virtual ICollection<Schedule> ScheduleCourses { get; set; } = new List<Schedule>();
}
