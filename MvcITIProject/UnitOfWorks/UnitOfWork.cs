using MvcITIProject.Models;
using MvcITIProject.Repositories;

namespace MvcITIProject.UnitOfWorks
{
    public class UnitOfWork
    {
        LibraryContext _context;

        public UnitOfWork(LibraryContext context)
        {
            _context = context;
            
        }
        private BookRepository _bookRepo;
        private CategoryRepository _categoryRepo;

        public BookRepository Bookrepo
        {
            get
            {
                if (_bookRepo == null)
                    _bookRepo = new BookRepository(_context);

                return _bookRepo;
            }
        }
        public CategoryRepository CategoryRepo
        {
            get
            {
                if (_categoryRepo == null)
                    _categoryRepo = new CategoryRepository(_context);
                return _categoryRepo;
            }
        }




        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
