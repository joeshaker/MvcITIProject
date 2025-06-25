using System;
using System.Collections.Generic;

namespace MvcITIProject.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public DateOnly? Dob { get; set; }

    public int? Salary { get; set; }

    public int? Bouns { get; set; }

    public int? SuperId { get; set; }

    public int? FloorNo { get; set; }

    public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();

    public virtual Floor? FloorNoNavigation { get; set; }

    public virtual ICollection<Floor> Floors { get; set; } = new List<Floor>();

    public virtual ICollection<Employee> InverseSuper { get; set; } = new List<Employee>();

    public virtual Employee? Super { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
