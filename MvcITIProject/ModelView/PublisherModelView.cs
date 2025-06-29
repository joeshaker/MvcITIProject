using MvcITIProject.Models;

namespace MvcITIProject.ModelView
{
    public class PublisherModelView
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int BookCount { get; set; }
        public virtual ICollection<Book>? Books { get; set; } = new List<Book>();
    }
}
