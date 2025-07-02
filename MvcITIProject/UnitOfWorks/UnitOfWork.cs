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
        private IGenericRepositries<Floor> _floors;

        public IGenericRepositries<Floor> Floors
        {
            get
            {
                return _floors ??= new GenericRepositries<Floor>(_context);
            }
        }


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

        private AuthorRepository _authorRepo;

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


        public AuthorRepository authorRepo
        {
            get
            {
                if (_authorRepo == null)
                    _authorRepo = new AuthorRepository(_context);

                return _authorRepo;
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
