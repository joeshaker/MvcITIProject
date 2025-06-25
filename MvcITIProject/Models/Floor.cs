using System;
using System.Collections.Generic;

namespace MvcITIProject.Models;

public partial class Floor
{
    public int Number { get; set; }

    public int NumBlocks { get; set; }

    public int? MgId { get; set; }

    public DateOnly? HiringDate { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Employee? Mg { get; set; }

    public virtual ICollection<Shelf> Shelves { get; set; } = new List<Shelf>();
}
