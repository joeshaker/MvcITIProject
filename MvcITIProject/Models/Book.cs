using System;
using System.Collections.Generic;

namespace MvcITIProject.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int ?CatId { get; set; }

    public int ?PublisherId { get; set; }

    public string ?ShelfCode { get; set; } = null!;

    //public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();

    public virtual Category Cat { get; set; } = null!;

    public virtual Publisher Publisher { get; set; } = null!;

    public virtual Shelf ShelfCodeNavigation { get; set; } = null!;

    public virtual ICollection<Author> ?Authors { get; set; } = new List<Author>();
}
