using MvcITIProject.Models;

namespace MvcITIProject.ModelView
{
    public class BookModelView
    {
        public string Title { get; set; } = null!;

        public int? CatId { get; set; }

        public int? PublisherId { get; set; }

        public string? ShelfCode { get; set; } = null!;


        public virtual Category Cat { get; set; } = null!;

        public virtual Publisher Publisher { get; set; } = null!;

        public virtual Shelf ShelfCodeNavigation { get; set; } = null!;

        public virtual ICollection<Author>? Authors { get; set; } = new List<Author>();
    }
}
