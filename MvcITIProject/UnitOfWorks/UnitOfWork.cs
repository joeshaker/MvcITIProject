using MvcITIProject.IRepositories;
using MvcITIProject.Models;
using MvcITIProject.Repositories;

namespace MvcITIProject.UnitOfWorks
{
    public class UnitOfWork
    {
        LibraryContext _context;

        private readonly Dictionary<Type, object> _repositories = new();
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

        private ShelfRepository _shelfRepo;
        public ShelfRepository ShelfRepo
        {
            get
            {
                if (_shelfRepo == null)
                    _shelfRepo = new ShelfRepository(_context);
                return _shelfRepo;
            }
        }

        public IGenericRepositries<T> Repositries<T>() where T : class
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
