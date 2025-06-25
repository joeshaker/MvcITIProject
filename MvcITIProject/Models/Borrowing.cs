using System;
using System.Collections.Generic;

namespace MvcITIProject.Models;

public partial class Borrowing
{
    public int EmpId { get; set; }

    public int BookId { get; set; }

    public string UserSsn { get; set; } = null!;

    public DateOnly BorrowDate { get; set; }

    public DateOnly DueDate { get; set; }

    public int? Amount { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Employee Emp { get; set; } = null!;

    public virtual User UserSsnNavigation { get; set; } = null!;
}
