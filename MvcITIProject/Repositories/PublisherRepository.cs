using Microsoft.EntityFrameworkCore;
using MvcITIProject.Models;
using MvcITIProject.ModelView;

namespace MvcITIProject.Repositories
{
    public class PublisherRepository: GenericRepositries<Publisher>
    {
        public PublisherRepository(LibraryContext context) : base(context)
        {
        }
        public int GetBookCount(int publisherId)
        {
            return _context.Books.Count(b => b.PublisherId == publisherId);
        }
        public List<Book> GetBooksByPublisherId(int publisherId)
        {
            return _context.Books
                .Where(b => b.PublisherId == publisherId)
                .ToList();
        }

        internal object Select(Func<object, PublisherModelView> value)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Publisher> GetPaginated(int page, int pageSize)
        {
            return _context.Publishers
                .OrderBy(c => c.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IEnumerable<Publisher> Search(string searchString)
        {
            return _context.Publishers
                .Where(c => c.Name.Contains(searchString))
                .OrderBy(c => c.Name)
                .ToList();
        }

        public int TotalCount()
        {
            return _context.Publishers.Count();
        }

        public int SearchCount(string searchString)
        {
            return _context.Publishers
                .Where(c => c.Name.Contains(searchString))
                .Count();
        }
    }
}
