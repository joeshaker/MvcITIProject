using System;
using System.Collections.Generic;

namespace MvcITIProject.Models;

public partial class Shelf
{
    public string Code { get; set; } = null!;

    public int ?FloorNum { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual Floor FloorNumNavigation { get; set; } = null!;
}
