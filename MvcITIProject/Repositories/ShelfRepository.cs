using MvcITIProject.Models;

namespace MvcITIProject.Repositories
{
    public class ShelfRepository : GenericRepositries<Shelf>
    {
        public ShelfRepository(LibraryContext context) : base(context)
        {
        }

        public Shelf? GetByCode(string code)
        {
            return _context.Set<Shelf>().FirstOrDefault(c=>c.Code==code);
        }
        public void DeleteByCode(string code)
        {
            Shelf? shelf = GetByCode(code);
            if (shelf != null)
            {
                _context.Set<Shelf>().Remove(shelf);
            }
        }
    }
}
