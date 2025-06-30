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

        private readonly Dictionary<Type, object> _repositories = new();
        public BookRepository BookRepo
        {
            get
            {
                if (_bookRepo == null)
                    _bookRepo = new BookRepository(_context);

                return _bookRepo;
            }
        }

        public IGenericRepositries<T> Repository<T>() where T : class
        {
            if (_repositories.TryGetValue(typeof(T), out var repo))
                return (IGenericRepositries<T>)repo;

            var newRepo = new GenericRepositries<T>(_context);
            _repositories.Add(typeof(T), newRepo);
            return newRepo;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
