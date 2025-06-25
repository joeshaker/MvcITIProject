using System;
using System.Collections.Generic;

namespace MvcITIProject.Models;

public partial class Category
{
    public int Id { get; set; }

    public string CatName { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
