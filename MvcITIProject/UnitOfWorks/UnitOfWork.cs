using MvcITIProject.IRepositories;
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
        private IGenericRepositries<Floor> _floors;

        public BookRepository Bookrepo
        {
            get
            {
                if (_bookRepo == null)
                    _bookRepo = new BookRepository(_context);

                return _bookRepo;
            }
        }
        public IGenericRepositries<Floor> Floors
        {
            get
            {
                return _floors ??= new GenericRepositries<Floor>(_context);
            }
        }



        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
