using MvcITIProject.Models;

namespace MvcITIProject.Repositories
{
    public class ShelfRepository:GenericRepositries<Shelf>
    {
        public ShelfRepository(LibraryContext context):base(context)
        {
        }
    }
}
