using MvcITIProject.Models;

namespace MvcITIProject.UnitOfWorks
{
    public class UnitOfWork
    {
        LibraryContext _context;

        public UnitOfWork(LibraryContext context)
        {
            _context = context;
            
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
