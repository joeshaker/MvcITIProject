namespace MvcITIProject.IRepositories
{
    public interface IGenericRepositries<T>
    {
        public List<T> GetAll();

        public T GetById(int id);

        public void Add(T entity);

        public void Update(T entity);

        public void Delete(int id);

    }
}
