using MvcITIProject.Models;

namespace MvcITIProject.Repositories
{
    public class BookRepository : GenericRepositries<Book>
    {
        public BookRepository(LibraryContext context) : base(context)
        {
        }
    }
}
