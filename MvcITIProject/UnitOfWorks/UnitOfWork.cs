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
        public BookRepository Bookrepo
        {
            get
            {
                if (_bookRepo == null)
                    _bookRepo = new BookRepository(_context);

                return _bookRepo;
            }
        }

        private PublisherRepository _publisherRepo;
        public PublisherRepository Publisherrepo
        {
            get
            {
                if (_publisherRepo == null)
                    _publisherRepo = new PublisherRepository(_context);
                return _publisherRepo;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
