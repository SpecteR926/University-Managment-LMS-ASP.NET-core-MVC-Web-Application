using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dbfirstapp.Models;

public partial class Teacher
{
    public int TId { get; set; }

    public string TName { get; set; } = null!;

    public DateOnly Dob { get; set; }
    [DataType(DataType.Password)]
    public string TPassword { get; set; } = null!;

    public long? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
