using MvcITIProject.Models;

namespace MvcITIProject.Repositories
{

    public class CategoryRepository : GenericRepositries<Category>
    {
        public CategoryRepository(LibraryContext context) : base(context)
        {

        }
        public int CountBooksInCategory(int categoryId)
        {
            return _context.Books.Count(b => b.CatId == categoryId);

        }
        // hellper methods for pagination and searching
        public IEnumerable<Category> GetPaginated(int page, int pageSize)
        {
            return _context.Categories
                .OrderBy(c => c.CatName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IEnumerable<Category> Search(string searchString)
        {
            return _context.Categories
                .Where(c => c.CatName.Contains(searchString))
                .OrderBy(c => c.CatName)
                .ToList();
        }

        public int TotalCount()
        {
            return _context.Categories.Count();
        }

        public int SearchCount(string searchString)
        {
            return _context.Categories
                .Where(c => c.CatName.Contains(searchString))
                .Count();
        }
    }
}
