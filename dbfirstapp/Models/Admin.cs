using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dbfirstapp.Models;

public partial class Admin
{
    public int AId { get; set; }
    [DataType(DataType.Password)]
    public string APassword { get; set; } = null!;
}
