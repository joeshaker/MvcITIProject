using Microsoft.EntityFrameworkCore;
using MvcITIProject.IRepositories;
using MvcITIProject.Models;

namespace MvcITIProject.Repositories
{
    public class GenericRepositries<TEntity> : IGenericRepositries<TEntity>
    where TEntity : class
    {
        public LibraryContext _context;
        public GenericRepositries(LibraryContext context) 
        {
            _context = context;
        }
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);

        }

        public void Delete(int id)
        {
            _context.Set<TEntity>().Remove(GetById(id));
        }

        public List<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
           return _context.Set<TEntity>().Find(id);

        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State=EntityState.Modified;
        }
    }
}
