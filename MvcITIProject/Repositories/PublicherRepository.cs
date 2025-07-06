using MvcITIProject.Models;

namespace MvcITIProject.Repositories
{
    public class PublicherRepository:GenericRepositries<Publisher>
    {
        public PublicherRepository(LibraryContext context):base(context)
        {
        }
    }
}
